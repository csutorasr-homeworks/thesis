using AutoMapper;
using Flottapp.Application.Fleet;
using Flottapp.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListCarsForFleetQuery : IRequest<IEnumerable<CarRowVm>>, IAuthorizableRequest
    {
        public string FleetId { get; set; }
        public string LicensePlateNumber { get; set; }
        public bool NeedsToBeServiced { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public AuthorizationData AuthorizationData { get; set; }
        public class Handler : IRequestHandler<ListCarsForFleetQuery, IEnumerable<CarRowVm>>
        {
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;

            public Handler(IFleetStore fleetStore, IMapper mapper)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<CarRowVm>> Handle(ListCarsForFleetQuery request, CancellationToken cancellationToken)
            {
                var data = await fleetStore.GetFleet(request.FleetId, request.AuthorizationData, cancellationToken);
                return mapper.Map<IEnumerable<CarRowVm>>(data.Cars.Where(x => x.Activated));
            }
        }
    }
}
