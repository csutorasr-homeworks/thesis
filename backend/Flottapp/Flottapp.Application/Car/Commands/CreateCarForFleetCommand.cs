using AutoMapper;
using Flottapp.Application.Fleet;
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
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;
            private readonly IDateTimeProvider dateTimeProvider;

            public Handler(IFleetStore fleetStore, IMapper mapper, IDateTimeProvider dateTimeProvider)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
                this.dateTimeProvider = dateTimeProvider;
            }
            public async Task<string> Handle(CreateCarForFleetCommand request, CancellationToken cancellationToken)
            {
                return await fleetStore.AddCarToFleet(request.FleetId, new Car
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
