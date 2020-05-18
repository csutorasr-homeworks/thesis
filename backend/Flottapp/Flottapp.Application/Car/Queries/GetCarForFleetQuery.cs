using AutoMapper;
using Flottapp.Application.Fleet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class GetCarForFleetQuery : IRequest<CarVm>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public class Handler : IRequestHandler<GetCarForFleetQuery, CarVm>
        {
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;

            public Handler(IFleetStore fleetStore, IMapper mapper)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
            }
            public async Task<CarVm> Handle(GetCarForFleetQuery request, CancellationToken cancellationToken)
            {
                var data = await fleetStore.GetCarForFleet(request.FleetId, request.CarId, cancellationToken);
                return mapper.Map<CarVm>(data);
            }
        }
    }
}
