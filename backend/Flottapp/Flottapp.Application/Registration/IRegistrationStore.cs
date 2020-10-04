using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.Registration
{
    public interface IRegistrationsStore
    {
        Task<IEnumerable<Domain.Registration>> GetRegistrations(string fleetId, string carId, CancellationToken cancellationToken);
        Task<string> AddRegistrationForCar(string fleetId, string carId, Domain.Registration registration, CancellationToken cancellationToken);
        Task DeleteRegistrationForCar(string fleetId, string carId, string registrationId, CancellationToken cancellationToken);
    }
}
