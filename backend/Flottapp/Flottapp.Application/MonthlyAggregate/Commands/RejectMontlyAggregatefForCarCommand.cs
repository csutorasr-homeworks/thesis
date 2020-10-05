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
    public class RejectMontlyAggregatefForCarCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public string AggregateId { get; set; }
        public class Handler : IRequestHandler<RejectMontlyAggregatefForCarCommand>
        {
            private readonly IMonthlyAggregatesStore monthlyAggregatesStore;
            private readonly IMapper mapper;

            public Handler(IMonthlyAggregatesStore monthlyAggregatesStore, IMapper mapper)
            {
                this.monthlyAggregatesStore = monthlyAggregatesStore;
                this.mapper = mapper;
            }
            public async Task<Unit> Handle(RejectMontlyAggregatefForCarCommand request, CancellationToken cancellationToken)
            {
                await monthlyAggregatesStore.RejectMonthlyAggregate(request.FleetId, request.CarId, request.AggregateId, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
