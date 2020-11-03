using Flottapp.Application.Fleet;
using Flottapp.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateFleetCommand : IRequest<string>, IAuthorizableRequest
    {
        public string Name { get; set; }
        public AuthorizationData AuthorizationData { get; set; }

        public class Handler : IRequestHandler<CreateFleetCommand, string>
        {
            private readonly IFleetStore fleetStore;

            public Handler(IFleetStore fleetStore)
            {
                this.fleetStore = fleetStore;
            }
            public Task<string> Handle(CreateFleetCommand request, CancellationToken cancellationToken)
            {
                return fleetStore.CreateFleet(request.Name, request.AuthorizationData, cancellationToken);
            }
        }
    }
}
