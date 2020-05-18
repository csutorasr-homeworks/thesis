using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.Fleet
{
    public interface IFleetStore
    {
        Task<List<Domain.Fleet>> GetFleets(CancellationToken cancellationToken);
        Task<string> CreateFleet(string name, CancellationToken cancellationToken);
        Task DeleteFleet(string id, CancellationToken cancellationToken);
        Task<Domain.Fleet> GetFleet(string id, CancellationToken cancellationToken);
        Task SaveName(Domain.Fleet fleet, CancellationToken cancellationToken);
        Task AddUserToFleet(string id, string userId, CancellationToken cancellationToken);
        Task RemoveUserFromFleet(string id, string userId, CancellationToken cancellationToken);
        Task<string> AddCarToFleet(string fleetId, Domain.Car car, CancellationToken cancellationToken);
        Task ModifyCarInFleet(string fleetId, Domain.Car car, CancellationToken cancellationToken);
        Task DeactivateCarForFleet(string fleetId, string carId, CancellationToken cancellationToken);
        Task<Domain.Car> GetCarForFleet(string fleetId, string carId, CancellationToken cancellationToken);
    }
}
