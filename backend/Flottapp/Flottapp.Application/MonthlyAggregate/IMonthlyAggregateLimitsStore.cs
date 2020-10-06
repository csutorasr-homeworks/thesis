using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Domain;

namespace Flottapp.Application.MonthlyAggregate
{
    public interface IMonthlyAggregateLimitsStore
    {
        Task CreateOrUpdateLimitForCar(string fleetId, string carId, Money limit, CancellationToken cancellationToken);
        Task<Money> GetLimitForCar(string fleetId, string carId, CancellationToken cancellationToken);
    }
}
