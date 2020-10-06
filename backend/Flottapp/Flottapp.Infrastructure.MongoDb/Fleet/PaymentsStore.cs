using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Application.MonthlyAggregate;
using Flottapp.Application.Payments;
using Flottapp.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Flottapp.Infrastructure.MongoDb.Fleet
{
    class PaymentsStore : AbstractStore<Payment>, IPaymentsStore
    {
        public PaymentsStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task AcceptPaymentForCar(string fleetId, string carId, string paymentId, DateTimeOffset now, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(
                x => x.FleetId == fleetId && x.CarId == carId && x.Accepted == null && x.Id == paymentId,
                Builders<Payment>.Update.Set(x => x.Accepted, now),
                cancellationToken: cancellationToken
            );
        }

        public async Task CreatePayment(string fleetId, string carId, Money price, DateTimeOffset now, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(new Payment
            {
                Accepted = null,
                CarId = carId,
                CreationTime = now,
                FleetId = fleetId,
                Sum = price,
            }, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsForCar(string fleetId, string carId, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(x => x.FleetId == fleetId && x.CarId == carId, cancellationToken: cancellationToken);
            return await result.ToListAsync(cancellationToken);
        }
    }
}
