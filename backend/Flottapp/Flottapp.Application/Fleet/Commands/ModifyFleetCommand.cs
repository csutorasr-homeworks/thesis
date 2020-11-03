using Flottapp.Application.Fleet;
using Flottapp.Model;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class ModifyFleetCommand : IRequest, IAuthorizableRequest
    {
        public string Id { get; set; }
        public Dto Data { get; set; }
        public AuthorizationData AuthorizationData { get; set; }
        public class Handler : IRequestHandler<ModifyFleetCommand>
        {
            private readonly IFleetStore fleetStore;

            public Handler(IFleetStore fleetStore)
            {
                this.fleetStore = fleetStore;
            }
            public async Task<Unit> Handle(ModifyFleetCommand request, CancellationToken cancellationToken)
            {
                var fleet = await fleetStore.GetFleet(request.Id, request.AuthorizationData, cancellationToken);
                fleet.Name = request.Data.Name;
                await fleetStore.SaveName(fleet, cancellationToken);
                return Unit.Value;
            }
        }

        public class Dto
        {
            public string Name { get; set; }
        }
    }
}
