using Flottapp.Application.Fleet;
using Flottapp.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteFleetCommand : IRequest, IAuthorizableRequest
    {
        public string Id { get; set; }
        public AuthorizationData AuthorizationData { get; set; }
        public class Handler : IRequestHandler<DeleteFleetCommand>
        {
            private readonly IFleetStore fleetStore;

            public Handler(IFleetStore fleetStore)
            {
                this.fleetStore = fleetStore;
            }
            public async Task<Unit> Handle(DeleteFleetCommand request, CancellationToken cancellationToken)
            {
                await fleetStore.DeleteFleet(request.Id, request.AuthorizationData, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
