using AutoMapper;
using Flottapp.Application.ServiceOccasions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class CreateServiceOccasionForCarCommand : IRequest<string>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public Dto Data { get; set; }

        public class Handler : IRequestHandler<CreateServiceOccasionForCarCommand, string>
        {
            private readonly IServiceOccasionsStore serviceOccasionsStore;
            private readonly IMapper mapper;

            public Handler(IServiceOccasionsStore serviceOccasionsStore, IMapper mapper)
            {
                this.serviceOccasionsStore = serviceOccasionsStore;
                this.mapper = mapper;
            }
            public async Task<string> Handle(CreateServiceOccasionForCarCommand request, CancellationToken cancellationToken)
            {
                var serviceOccasion = new Domain.ServiceOccasion
                {
                    Mileage = request.Data.Mileage,
                    CreationTime = request.Data.DateTime,
                    Type = mapper.Map<Domain.ServiceType>(request.Data.Type),
                };
                var serviceOccasionId = await serviceOccasionsStore.AddServiceOccasionForCar(request.FleetId, request.CarId, serviceOccasion, cancellationToken);
                return serviceOccasionId;
            }
        }
        public class Dto
        {
            public decimal Mileage { get; set; }
            public DateTimeOffset DateTime { get; set; }
            public ServiceType Type { get; set; }
        }
    }
}
