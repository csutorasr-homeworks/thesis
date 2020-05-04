using AutoMapper;
using Flottapp.Application.Fleet;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListFleetsQuery : IRequest<IEnumerable<FleetRowVm>>
    {
        public class ListFleetsQueryHandler : IRequestHandler<ListFleetsQuery, IEnumerable<FleetRowVm>>
        {
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;

            public ListFleetsQueryHandler(IFleetStore fleetStore, IMapper mapper)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<FleetRowVm>> Handle(ListFleetsQuery request, CancellationToken cancellationToken)
            {
                var data = await fleetStore.GetFleets(cancellationToken);
                return data.Select(x => mapper.Map<FleetRowVm>(x));
            }
        }
    }
}
