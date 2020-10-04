using AutoMapper;
using Flottapp.Application.Car;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class GetCarForFleetQuery : IRequest<CarVm>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public class Handler : IRequestHandler<GetCarForFleetQuery, CarVm>
        {
            private readonly ICarsStore carsStore;
            private readonly IMapper mapper;

            public Handler(ICarsStore carsStore, IMapper mapper)
            {
                this.carsStore = carsStore;
                this.mapper = mapper;
            }
            public async Task<CarVm> Handle(GetCarForFleetQuery request, CancellationToken cancellationToken)
            {
                var data = await carsStore.GetCarForFleet(request.FleetId, request.CarId, cancellationToken);
                return mapper.Map<CarVm>(data);
            }
        }
    }
}
