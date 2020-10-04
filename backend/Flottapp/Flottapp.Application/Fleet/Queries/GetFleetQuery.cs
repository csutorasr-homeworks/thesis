﻿using AutoMapper;
using Flottapp.Application.Fleet;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class GetFleetQuery : IRequest<FleetVm>
    {
        public string Id { get; set; }
        public class Handler : IRequestHandler<GetFleetQuery, FleetVm>
        {
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;

            public Handler(IFleetStore fleetStore, IMapper mapper)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
            }
            public async Task<FleetVm> Handle(GetFleetQuery request, CancellationToken cancellationToken)
            {
                var data = await fleetStore.GetFleet(request.Id, cancellationToken);
                return mapper.Map<FleetVm>(data);
            }
        }
    }
}
