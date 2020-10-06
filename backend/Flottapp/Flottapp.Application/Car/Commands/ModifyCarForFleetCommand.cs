using AutoMapper;
using Flottapp.Application.Car;
using Flottapp.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class ModifyCarForFleetCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public Dto Data { get; set; }
        public class Handler : IRequestHandler<ModifyCarForFleetCommand>
        {
            private readonly ICarsStore carsStore;
            private readonly IMapper mapper;
            private readonly IDateTimeProvider dateTimeProvider;
            private readonly IMediator mediator;

            public Handler(ICarsStore carsStore, IMapper mapper, IDateTimeProvider dateTimeProvider, IMediator mediator)
            {
                this.carsStore = carsStore;
                this.mapper = mapper;
                this.dateTimeProvider = dateTimeProvider;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(ModifyCarForFleetCommand request, CancellationToken cancellationToken)
            {
                var car = await carsStore.GetCarForFleet(request.FleetId, request.CarId, cancellationToken);
                var newLimit = mapper.Map<Money>(request.Data.LimitPerMonth);
                var limitChanged = newLimit.Currency != car.LimitPerMonth.Currency || newLimit.Value != car.LimitPerMonth.Value;
                car.LimitPerMonth = newLimit;
                car.LicensePlateNumber = request.Data.LicensePlateNumber;
                await carsStore.ModifyCarInFleet(request.FleetId, car, cancellationToken);
                if (limitChanged)
                {
                    var @event = mapper.Map<CarLimitChangedEvent>(car);
                    @event.FleetId = request.FleetId;
                    await mediator.Publish(@event, cancellationToken: cancellationToken);
                }
                return Unit.Value;
            }
        }

        public class Dto
        {
            public MoneyVm LimitPerMonth { get; set; }
            public string LicensePlateNumber { get; set; }
        }
    }
}
