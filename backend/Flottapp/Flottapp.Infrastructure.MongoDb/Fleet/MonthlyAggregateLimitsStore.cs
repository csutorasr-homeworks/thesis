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
    class MonthlyAggregateLimitsStore : AbstractStore<MonthlyAggregateLimit>, IMonthlyAggregateLimitsStore
    {
        public MonthlyAggregateLimitsStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task CreateOrUpdateLimitForCar(string fleetId, string carId, Money limit, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(
                x => x.FleetId == fleetId && x.CarId == carId,
                Builders<MonthlyAggregateLimit>.Update
                    .Set(x => x.Limit, limit)
                    .SetOnInsert(x => x.Id, ObjectId.GenerateNewId().ToString())
                    .SetOnInsert(x => x.CarId, carId)
                    .SetOnInsert(x => x.FleetId, fleetId),
                new UpdateOptions
                {
                    IsUpsert = true,
                },
                cancellationToken: cancellationToken
            );
        }

        public async Task<Money> GetLimitForCar(string fleetId, string carId, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(x => x.FleetId == fleetId && x.CarId == carId, cancellationToken: cancellationToken);
            return (await result.FirstOrDefaultAsync(cancellationToken))?.Limit;
        }
    }
}
