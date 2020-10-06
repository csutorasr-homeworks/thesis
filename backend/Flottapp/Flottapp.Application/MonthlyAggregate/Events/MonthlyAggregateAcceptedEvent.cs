using System;
using MediatR;

namespace Flottapp.Application.MonthlyAggregate
{
    public class MonthlyAggregateAcceptedEvent : INotification
    {
        public Domain.MontlyAggregate MontlyAggregate { get; set; }
    }
}
