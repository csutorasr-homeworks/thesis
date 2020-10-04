using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.Car
{
    public interface ICarsStore
    {
        Task<string> AddCarToFleet(string fleetId, Domain.Car car, CancellationToken cancellationToken);
        Task DeactivateCarForFleet(string fleetId, string carId, CancellationToken cancellationToken);
        Task<Domain.Car> GetCarForFleet(string fleetId, string carId, CancellationToken cancellationToken);
        Task ModifyCarInFleet(string fleetId, Domain.Car car, CancellationToken cancellationToken);
    }
}