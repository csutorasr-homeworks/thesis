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
            private readonly IMediator mediator;
            private readonly IDateTimeProvider dateTimeProvider;

            public Handler(ICarsStore carsStore, IMapper mapper, IMediator mediator, IDateTimeProvider dateTimeProvider)
            {
                this.carsStore = carsStore;
                this.mapper = mapper;
                this.mediator = mediator;
                this.dateTimeProvider = dateTimeProvider;
            }
            public async Task<string> Handle(CreateCarForFleetCommand request, CancellationToken cancellationToken)
            {
                var car = new Car
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Activated = true,
                    LicensePlateNumber = request.Data.LicensePlateNumber,
                    LimitPerMonth = mapper.Map<Money>(request.Data.LimitPerMonth),
                    CreationTime = dateTimeProvider.Now(),
                };
                string carId = await carsStore.AddCarToFleet(request.FleetId, car, cancellationToken);
                var @event = mapper.Map<CarLimitChangedEvent>(car);
                @event.FleetId = request.FleetId;
                await mediator.Publish(@event, cancellationToken: cancellationToken);
                return carId;
            }
        }

        public class Dto
        {
            public MoneyVm LimitPerMonth { get; set; }
            public string LicensePlateNumber { get; set; }
        }
    }
}
