using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Application.Registration;
using Flottapp.Domain;
using MongoDB.Driver;

namespace Flottapp.Infrastructure.MongoDb.Fleet
{
    class RegistrationsStore : AbstractStore<Domain.Registration>, IRegistrationsStore
    {
        public RegistrationsStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task<string> AddRegistrationForCar(string fleetId, string carId, Registration registration, CancellationToken cancellationToken)
        {
            registration.FleetId = fleetId;
            registration.CarId = carId;
            await _collection.InsertOneAsync(registration, cancellationToken: cancellationToken);
            return registration.Id;
        }

        public async Task DeleteRegistrationForCar(string fleetId, string carId, string registrationId, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(x => x.FleetId == fleetId && x.CarId == carId && x.Id == registrationId, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Registration>> GetRegistrations(string fleetId, string carId, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(x => x.FleetId == fleetId && x.CarId == carId, cancellationToken: cancellationToken);
            return await result.ToListAsync(cancellationToken);
        }
    }
}
