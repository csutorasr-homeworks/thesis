using AutoMapper;
using Flottapp.Application.Fleet;
using Flottapp.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListFleetsQuery : IRequest<IEnumerable<FleetRowVm>>, IAuthorizableRequest
    {
        public AuthorizationData AuthorizationData { get; set; }

        public class Handler : IRequestHandler<ListFleetsQuery, IEnumerable<FleetRowVm>>
        {
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;

            public Handler(IFleetStore fleetStore, IMapper mapper)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<FleetRowVm>> Handle(ListFleetsQuery request, CancellationToken cancellationToken)
            {
                var data = await fleetStore.GetFleets(request.AuthorizationData, cancellationToken);
                return data.Select(x => mapper.Map<FleetRowVm>(x));
            }
        }
    }
}
