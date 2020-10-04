using AutoMapper;
using Flottapp.Application.Car;
using Flottapp.Domain;
using MediatR;
using MongoDB.Bson;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateCarForFleetCommand : IRequest<string>
    {
        public string FleetId { get; set; }
        public Dto Data { get; set; }
        public class Handler : IRequestHandler<CreateCarForFleetCommand, string>
        {
            private readonly ICarsStore carsStore;
            private readonly IMapper mapper;
            private readonly IDateTimeProvider dateTimeProvider;

            public Handler(ICarsStore carsStore, IMapper mapper, IDateTimeProvider dateTimeProvider)
            {
                this.carsStore = carsStore;
                this.mapper = mapper;
                this.dateTimeProvider = dateTimeProvider;
            }
            public async Task<string> Handle(CreateCarForFleetCommand request, CancellationToken cancellationToken)
            {
                return await carsStore.AddCarToFleet(request.FleetId, new Car
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Activated = true,
                    LicensePlateNumber = request.Data.LicensePlateNumber,
                    LimitPerMonth = mapper.Map<Money>(request.Data.LimitPerMonth),
                    CreationTime = dateTimeProvider.Now(),
                }, cancellationToken);
            }
        }

        public class Dto
        {
            public MoneyVm LimitPerMonth { get; set; }
            public string LicensePlateNumber { get; set; }
        }
    }
}
