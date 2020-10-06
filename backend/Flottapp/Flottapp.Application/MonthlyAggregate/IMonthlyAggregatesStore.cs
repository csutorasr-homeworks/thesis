using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Domain;

namespace Flottapp.Application.MonthlyAggregate
{
    public interface IMonthlyAggregatesStore
    {
        Task<IEnumerable<Domain.MontlyAggregate>> GetMonthlyAggregates(string fleetId, string carId, CancellationToken cancellationToken);
        Task AcceptMonthlyAggregate(string fleetId, string carId, string monthlyAggregateId, Money limit, CancellationToken cancellationToken);
        Task RejectMonthlyAggregate(string fleetId, string carId, string monthlyAggregateId, CancellationToken cancellationToken);
        Task AddNewRegistrationForCar(Domain.Registration registration, CancellationToken cancellationToken);
        Task RemoveRegistrationForCar(string fleetId, string carId, string registrationId, CancellationToken cancellationToken);
        Task<MontlyAggregate> GetMonthlyAggregateById(string aggregateId, CancellationToken cancellationToken);
    }
}
