using AutoMapper;
using Flottapp.Application.Registration;
using Flottapp.Domain;
using MediatR;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateRegistrationForCarCommand : IRequest<string>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public Dto Data { get; set; }

        public class Handler : IRequestHandler<CreateRegistrationForCarCommand, string>
        {
            private readonly IRegistrationsStore registrationStore;
            private readonly IMapper mapper;
            private readonly IMediator mediator;

            public Handler(IRegistrationsStore registrationStore, IMapper mapper, IMediator mediator)
            {
                this.registrationStore = registrationStore;
                this.mapper = mapper;
                this.mediator = mediator;
            }
            public async Task<string> Handle(CreateRegistrationForCarCommand request, CancellationToken cancellationToken)
            {
                var registration = new Registration
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Location = mapper.Map<Location>(request.Data.Location),
                    Mileage = request.Data.Mileage,
                    Price = mapper.Map<Money>(request.Data.Price),
                    RefuelQuantity = request.Data.RefuelQuantity,
                    CreationTime = request.Data.Time,
                };
                var registrationId = await registrationStore.AddRegistrationForCar(request.FleetId, request.CarId, registration, cancellationToken);
                await mediator.Publish(mapper.Map<CreateRegistrationForCarEvent>(registration), cancellationToken: cancellationToken);
                return registrationId;
            }
        }

        public class Dto
        {
            public DateTimeOffset Time { get; set; }
            public decimal Mileage { get; set; }
            public LocationVm Location { get; set; }
            public decimal RefuelQuantity { get; set; }
            public MoneyVm Price { get; set; }
        }
    }
}
