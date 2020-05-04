using Flottapp.Application.Fleet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteFleetCommand : IRequest
    {
        public string Id { get; set; }
        public class Handler : IRequestHandler<DeleteFleetCommand>
        {
            private readonly IFleetStore fleetStore;

            public Handler(IFleetStore fleetStore)
            {
                this.fleetStore = fleetStore;
            }
            public async Task<Unit> Handle(DeleteFleetCommand request, CancellationToken cancellationToken)
            {
                await fleetStore.DeleteFleet(request.Id, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
