using Flottapp.Application.Fleet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class AddUserToFleetCommand : IRequest
    {
        public string Id { get; set; }
        public Dto Data { get; set; }
        public class Handler : IRequestHandler<AddUserToFleetCommand>
        {
            private readonly IFleetStore fleetStore;

            public Handler(IFleetStore fleetStore)
            {
                this.fleetStore = fleetStore;
            }
            public async Task<Unit> Handle(AddUserToFleetCommand request, CancellationToken cancellationToken)
            {
                await fleetStore.AddUserToFleet(request.Id, request.Data.UserId, cancellationToken);
                return Unit.Value;
            }
        }

        public class Dto
        {
            public string UserId { get; set; }
        }
    }
}
