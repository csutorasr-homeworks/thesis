using AutoMapper;
using Flottapp.Application.Fleet;
using Flottapp.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
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
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;
            private readonly IDateTimeProvider dateTimeProvider;

            public Handler(IFleetStore fleetStore, IMapper mapper, IDateTimeProvider dateTimeProvider)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
                this.dateTimeProvider = dateTimeProvider;
            }
            public async Task<Unit> Handle(ModifyCarForFleetCommand request, CancellationToken cancellationToken)
            {
                var car = await fleetStore.GetCarForFleet(request.FleetId, request.CarId, cancellationToken);
                car.LimitPerMonth = mapper.Map<Money>(request.Data.LimitPerMonth);
                car.LicensePlateNumber = request.Data.LicensePlateNumber;
                await fleetStore.ModifyCarInFleet(request.FleetId, car, cancellationToken);
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
