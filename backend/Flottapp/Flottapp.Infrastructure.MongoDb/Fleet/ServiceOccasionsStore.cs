using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Application.Registration;
using Flottapp.Application.ServiceOccasions;
using Flottapp.Domain;
using MongoDB.Driver;

namespace Flottapp.Infrastructure.MongoDb.Fleet
{
    class ServiceOccasionsStore : AbstractStore<Domain.ServiceOccasion>, IServiceOccasionsStore
    {
        public ServiceOccasionsStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task<string> AddServiceOccasionForCar(string fleetId, string carId, ServiceOccasion serviceOccasion, CancellationToken cancellationToken)
        {
            serviceOccasion.FleetId = fleetId;
            serviceOccasion.CarId = carId;
            await _collection.InsertOneAsync(serviceOccasion, cancellationToken: cancellationToken);
            return serviceOccasion.Id;
        }

        public async Task DeleteServiceOccasionForCar(string fleetId, string carId, string occasionId, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(x => x.FleetId == fleetId && x.CarId == carId && x.Id == occasionId, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<ServiceOccasion>> GetServiceOccasionForCar(string fleetId, string carId, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(x => x.FleetId == fleetId && x.CarId == carId, cancellationToken: cancellationToken);
            return await result.ToListAsync(cancellationToken);
        }
    }
}
