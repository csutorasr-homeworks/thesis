using AutoMapper;
using Flottapp.Application.MonthlyAggregate;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListMonthlyAggregatesForCarQuery : IRequest<IEnumerable<MonthlyAggregateRowVm>>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public int PageSize { get; set; }
        public int PageLength { get; set; }
        public class Handler : IRequestHandler<ListMonthlyAggregatesForCarQuery, IEnumerable<MonthlyAggregateRowVm>>
        {
            private readonly IMonthlyAggregatesStore monthlyAggregatesStore;
            private readonly IMapper mapper;

            public Handler(IMonthlyAggregatesStore monthlyAggregatesStore, IMapper mapper)
            {
                this.monthlyAggregatesStore = monthlyAggregatesStore;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<MonthlyAggregateRowVm>> Handle(ListMonthlyAggregatesForCarQuery request, CancellationToken cancellationToken)
            {
                var data = await monthlyAggregatesStore.GetMonthlyAggregates(request.FleetId, request.CarId, cancellationToken);
                return mapper.Map<IEnumerable<MonthlyAggregateRowVm>>(data);
            }
        }
    }
}
