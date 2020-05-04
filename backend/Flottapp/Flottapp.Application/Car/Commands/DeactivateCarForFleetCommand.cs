﻿using AutoMapper;
using Flottapp.Application.Fleet;
using Flottapp.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class DeactivateCarForFleetCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public class Handler : IRequestHandler<DeactivateCarForFleetCommand>
        {
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;

            public Handler(IFleetStore fleetStore, IMapper mapper)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
            }
            public async Task<Unit> Handle(DeactivateCarForFleetCommand request, CancellationToken cancellationToken)
            {
                await fleetStore.DeactivateCarForFleet(request.FleetId, request.CarId, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
