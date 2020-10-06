using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.ServiceRules

{
    public interface IServiceRulesStore
    {
        Task<IEnumerable<Domain.ServiceRule>> GetServiceRulesForCar(string fleetId, string carId, CancellationToken cancellationToken);
        Task<string> CreateMileageServiceRuleForCar(string fleetId, string carId, decimal travelledMileage, CancellationToken cancellationToken);
        Task<string> CreateTimeServiceRuleForCar(string fleetId, string carId, int intervalInMonth, CancellationToken cancellationToken);
        Task DeleteServiceRuleForCar(string fleetId, string carId, string serivceRuleId, CancellationToken cancellationToken);
    }
}
