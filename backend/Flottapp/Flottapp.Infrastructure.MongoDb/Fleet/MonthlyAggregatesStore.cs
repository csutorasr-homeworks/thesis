using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Application.MonthlyAggregate;
using Flottapp.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Flottapp.Infrastructure.MongoDb.Fleet
{
    class MonthlyAggregatesStore : AbstractStore<MontlyAggregate>, IMonthlyAggregatesStore
    {
        public MonthlyAggregatesStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task AcceptMonthlyAggregate(string fleetId, string carId, string monthlyAggregateId, Money limit, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(
                x => x.Id == monthlyAggregateId && x.FleetId == fleetId && x.CarId == carId && x.Accepted == null,
                Builders<MontlyAggregate>.Update
                    .Set(x => x.Accepted, true)
                    .Set(x => x.Limit, limit),
                cancellationToken: cancellationToken
            );
            if (result.ModifiedCount == 0)
            {
                throw new MonthlyAggregateAlreadyAcceptedException();
            }
        }

        public async Task AddNewRegistrationForCar(Registration registration, CancellationToken cancellationToken)
        {
            var month = registration.CreationTime.Month;
            var year = registration.CreationTime.Year;
            await _collection.UpdateOneAsync(
                x => x.FleetId == registration.FleetId && x.CarId == registration.CarId && x.Month == month && x.Year == year && x.Accepted == null,
                Builders<MontlyAggregate>.Update
                    .Set(x => x.Accepted, null)
                    .SetOnInsert(x => x.Id, ObjectId.GenerateNewId().ToString())
                    .SetOnInsert(x => x.CarId, registration.CarId)
                    .SetOnInsert(x => x.FleetId, registration.FleetId)
                    .SetOnInsert(x => x.Month, month)
                    .SetOnInsert(x => x.Year, year)
                    .AddToSet(x => x.Registrations, registration),
                new UpdateOptions
                {
                    IsUpsert = true,
                },
                cancellationToken: cancellationToken
            );
        }

        public async Task<MontlyAggregate> GetMonthlyAggregateById(string aggregateId, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(x => x.Id == aggregateId, cancellationToken: cancellationToken);
            return await result.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<MontlyAggregate>> GetMonthlyAggregates(string fleetId, string carId, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(x => x.FleetId == fleetId && x.CarId == carId, cancellationToken: cancellationToken);
            return await result.ToListAsync(cancellationToken);
        }

        public async Task RejectMonthlyAggregate(string fleetId, string carId, string monthlyAggregateId, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(
                x => x.Id == monthlyAggregateId && x.FleetId == fleetId && x.CarId == carId && x.Accepted == null,
                Builders<MontlyAggregate>.Update.Set(x => x.Accepted, false),
                cancellationToken: cancellationToken
            );
        }

        public async Task RemoveRegistrationForCar(string fleetId, string carId, string registrationId, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(
                x => x.FleetId == fleetId && x.CarId == carId && x.Registrations.Any(y => y.Id == registrationId),
                Builders<MontlyAggregate>.Update.PullFilter(x => x.Registrations, Builders<Registration>.Filter.Eq(x => x.Id, registrationId)),
                cancellationToken: cancellationToken
            );
            await _collection.DeleteManyAsync(Builders<MontlyAggregate>.Filter.Size(x => x.Registrations, 0), cancellationToken: cancellationToken);
        }
    }
}
