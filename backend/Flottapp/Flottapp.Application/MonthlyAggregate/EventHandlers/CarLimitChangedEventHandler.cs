using Flottapp.Application.Car;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.MonthlyAggregate
{
    public class CarLimitChangedEventHandler : INotificationHandler<CarLimitChangedEvent>
    {
        private readonly IMonthlyAggregateLimitsStore monthlyAggregateLimitsStore;

        public CarLimitChangedEventHandler(IMonthlyAggregateLimitsStore monthlyAggregateLimitsStore)
        {
            this.monthlyAggregateLimitsStore = monthlyAggregateLimitsStore;
        }

        public async Task Handle(CarLimitChangedEvent notification, CancellationToken cancellationToken)
        {
            await monthlyAggregateLimitsStore.CreateOrUpdateLimitForCar(notification.FleetId, notification.CarId, notification.Limit, cancellationToken);
        }
    }
}
