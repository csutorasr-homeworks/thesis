using AutoMapper;
using Flottapp.Application.ServiceRules;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListServiceRulesForCarQuery : IRequest<IEnumerable<ServiceRuleVm>>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public class Handler : IRequestHandler<ListServiceRulesForCarQuery, IEnumerable<ServiceRuleVm>>
        {
            private readonly IServiceRulesStore serviceRulesStore;
            private readonly IMapper mapper;

            public Handler(IServiceRulesStore serviceRulesStore, IMapper mapper)
            {
                this.serviceRulesStore = serviceRulesStore;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<ServiceRuleVm>> Handle(ListServiceRulesForCarQuery request, CancellationToken cancellationToken)
            {
                var data = await serviceRulesStore.GetServiceRulesForCar(request.FleetId, request.CarId, cancellationToken);
                return mapper.Map<IEnumerable<ServiceRuleVm>>(data);
            }
        }
    }
}
