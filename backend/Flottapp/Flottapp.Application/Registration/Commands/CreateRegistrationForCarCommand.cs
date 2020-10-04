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
            private readonly IDateTimeProvider dateTimeProvider;

            public Handler(IRegistrationsStore registrationStore, IMapper mapper, IDateTimeProvider dateTimeProvider)
            {
                this.registrationStore = registrationStore;
                this.mapper = mapper;
                this.dateTimeProvider = dateTimeProvider;
            }
            public async Task<string> Handle(CreateRegistrationForCarCommand request, CancellationToken cancellationToken)
            {
                return await registrationStore.AddRegistrationForCar(request.FleetId, request.CarId, new Registration
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Location = mapper.Map<Location>(request.Data.Location),
                    Mileage = request.Data.Mileage,
                    Price = mapper.Map<Money>(request.Data.Price),
                    RefuelQuantity = request.Data.RefuelQuantity,
                    CreationTime = request.Data.Time,
                }, cancellationToken);
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
