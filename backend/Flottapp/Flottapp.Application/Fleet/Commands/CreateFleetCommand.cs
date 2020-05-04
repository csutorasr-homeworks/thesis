using Flottapp.Application.Fleet;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateFleetCommand : IRequest<string>
    {
        public string Name { get; set; }
        public class Handler : IRequestHandler<CreateFleetCommand, string>
        {
            private readonly IFleetStore fleetStore;

            public Handler(IFleetStore fleetStore)
            {
                this.fleetStore = fleetStore;
            }
            public Task<string> Handle(CreateFleetCommand request, CancellationToken cancellationToken)
            {
                return fleetStore.CreateFleet(request.Name, cancellationToken);
            }
        }
    }
}
