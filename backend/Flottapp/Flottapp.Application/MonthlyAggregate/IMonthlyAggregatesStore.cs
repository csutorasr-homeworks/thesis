using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.MonthlyAggregate
{
    public interface IMonthlyAggregatesStore
    {
        Task<IEnumerable<Domain.MontlyAggregate>> GetMonthlyAggregates(string fleetId, string carId, CancellationToken cancellationToken);
        Task AcceptMonthlyAggregate(string fleetId, string carId, string monthlyAggregateId, CancellationToken cancellationToken);
        Task RejectMonthlyAggregate(string fleetId, string carId, string monthlyAggregateId, CancellationToken cancellationToken);
        Task AddNewRegistrationForCar(Domain.Registration registration, CancellationToken cancellationToken);
        Task RemoveRegistrationForCar(string fleetId, string carId, string registrationId, CancellationToken cancellationToken);
    }
}
