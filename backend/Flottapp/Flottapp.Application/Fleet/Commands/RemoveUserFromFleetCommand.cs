using Flottapp.Application.Fleet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class RemoveUserFromFleetCommand : IRequest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public class Handler : IRequestHandler<RemoveUserFromFleetCommand>
        {
            private readonly IFleetStore fleetStore;

            public Handler(IFleetStore fleetStore)
            {
                this.fleetStore = fleetStore;
            }
            public async Task<Unit> Handle(RemoveUserFromFleetCommand request, CancellationToken cancellationToken)
            {
                await fleetStore.RemoveUserFromFleet(request.Id, request.UserId, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
