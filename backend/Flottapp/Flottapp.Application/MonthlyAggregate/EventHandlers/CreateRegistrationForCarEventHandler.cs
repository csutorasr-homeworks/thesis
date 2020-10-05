using AutoMapper;
using Flottapp.Application.Registration;
using Flottapp.Domain;
using Flottapp.Infrastucture.Commands;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.MonthlyAggregate
{
    public class CreateRegistrationForCarEventHandler : INotificationHandler<CreateRegistrationForCarEvent>
    {
        private readonly IMonthlyAggregatesStore monthlyAggregatesStore;

        public CreateRegistrationForCarEventHandler(IMonthlyAggregatesStore monthlyAggregatesStore)
        {
            this.monthlyAggregatesStore = monthlyAggregatesStore;
        }

        public async Task Handle(CreateRegistrationForCarEvent notification, CancellationToken cancellationToken)
        {
            await monthlyAggregatesStore.AddNewRegistrationForCar(notification.Registration, cancellationToken);
        }
    }
}
