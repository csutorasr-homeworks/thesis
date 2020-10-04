using AutoMapper;
using Flottapp.Application.Car;
using MediatR;
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
            private readonly ICarsStore carsStore;
            private readonly IMapper mapper;

            public Handler(ICarsStore carsStore, IMapper mapper)
            {
                this.carsStore = carsStore;
                this.mapper = mapper;
            }
            public async Task<Unit> Handle(DeactivateCarForFleetCommand request, CancellationToken cancellationToken)
            {
                await carsStore.DeactivateCarForFleet(request.FleetId, request.CarId, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
