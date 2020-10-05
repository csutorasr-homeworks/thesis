using AutoMapper;
using Flottapp.Application.MonthlyAggregate;
using Flottapp.Application.Registration;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteRegistrationForCarEventHandler : INotificationHandler<DeleteRegistrationForCarEvent>
    {
        private readonly IMonthlyAggregatesStore monthlyAggregatesStore;

        public DeleteRegistrationForCarEventHandler(IMonthlyAggregatesStore monthlyAggregatesStore)
        {
            this.monthlyAggregatesStore = monthlyAggregatesStore;
        }

        public async Task Handle(DeleteRegistrationForCarEvent notification, CancellationToken cancellationToken)
        {
            await monthlyAggregatesStore.RemoveRegistrationForCar(notification.FleetId, notification.CarId, notification.RegistrationId, cancellationToken);
        }
    }
}
