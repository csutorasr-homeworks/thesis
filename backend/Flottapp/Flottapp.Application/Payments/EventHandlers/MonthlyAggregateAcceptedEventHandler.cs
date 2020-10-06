using Flottapp.Application.Payments;
using Flottapp.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.MonthlyAggregate
{
    public class MonthlyAggregateAcceptedEventHandler : INotificationHandler<MonthlyAggregateAcceptedEvent>
    {
        private readonly IPaymentsStore paymentsStore;
        private readonly IDateTimeProvider dateTimeProvider;

        public MonthlyAggregateAcceptedEventHandler(IPaymentsStore paymentsStore, IDateTimeProvider dateTimeProvider)
        {
            this.paymentsStore = paymentsStore;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task Handle(MonthlyAggregateAcceptedEvent @event, CancellationToken cancellationToken)
        {
            var monthlyAggregate = @event.MontlyAggregate;
            var sum = monthlyAggregate.Registrations.Select(x => x.Price).Aggregate(new List<Money>(), (acc, price) =>
            {
                var money = acc.Find(x => x.Currency == price.Currency);
                if (money == null)
                {
                    money = new Money
                    {
                        Currency = price.Currency,
                    };
                    acc.Add(money);
                }
                money.Value += price.Value;
                return acc;
            });
            foreach (var price in sum)
            {
                await paymentsStore.CreatePayment(monthlyAggregate.FleetId, monthlyAggregate.CarId, price, dateTimeProvider.Now(), cancellationToken);
            }
        }
    }
}
