using AutoMapper;
using Flottapp.Application.MonthlyAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class AcceptMontlyAggregatefForCarCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public string AggregateId { get; set; }
        public class Handler : IRequestHandler<AcceptMontlyAggregatefForCarCommand>
        {
            private readonly IMonthlyAggregatesStore monthlyAggregatesStore;
            private readonly IMapper mapper;
            private readonly IMediator mediator;
            private readonly IMonthlyAggregateLimitsStore monthlyAggregateLimitsStore;

            public Handler(IMonthlyAggregatesStore monthlyAggregatesStore, IMapper mapper, IMediator mediator, IMonthlyAggregateLimitsStore monthlyAggregateLimitsStore)
            {
                this.monthlyAggregatesStore = monthlyAggregatesStore;
                this.mapper = mapper;
                this.mediator = mediator;
                this.monthlyAggregateLimitsStore = monthlyAggregateLimitsStore;
            }
            public async Task<Unit> Handle(AcceptMontlyAggregatefForCarCommand request, CancellationToken cancellationToken)
            {
                var limit = await monthlyAggregateLimitsStore.GetLimitForCar(request.FleetId, request.CarId, cancellationToken);
                await monthlyAggregatesStore.AcceptMonthlyAggregate(request.FleetId, request.CarId, request.AggregateId, limit, cancellationToken);
                var @event = mapper.Map<MonthlyAggregateAcceptedEvent>(await monthlyAggregatesStore.GetMonthlyAggregateById(request.AggregateId, cancellationToken));
                await mediator.Publish(@event, cancellationToken: cancellationToken);
                return Unit.Value;
            }
        }
    }
}
