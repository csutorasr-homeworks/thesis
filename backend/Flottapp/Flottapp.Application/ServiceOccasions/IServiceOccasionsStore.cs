using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Domain;

namespace Flottapp.Application.ServiceOccasions
{
    public interface IServiceOccasionsStore
    {
        Task<string> AddServiceOccasionForCar(string fleetId, string carId, ServiceOccasion serviceOccasion, CancellationToken cancellationToken);
        Task DeleteServiceOccasionForCar(string fleetId, string carId, string occasionId, CancellationToken cancellationToken);
        Task<IEnumerable<ServiceOccasion>> GetServiceOccasionForCar(string fleetId, string carId, CancellationToken cancellationToken);
    }
}
