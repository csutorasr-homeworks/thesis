using AutoMapper;
using Flottapp.Application.Registration;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class ListRegistrationsForCarQuery : IRequest<IEnumerable<RegistrationVm>>
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public class Handler : IRequestHandler<ListRegistrationsForCarQuery, IEnumerable<RegistrationVm>>
        {
            private readonly IRegistrationsStore registrationStore;
            private readonly IMapper mapper;

            public Handler(IRegistrationsStore registrationStore, IMapper mapper)
            {
                this.registrationStore = registrationStore;
                this.mapper = mapper;
            }
            public async Task<IEnumerable<RegistrationVm>> Handle(ListRegistrationsForCarQuery request, CancellationToken cancellationToken)
            {
                var data = await registrationStore.GetRegistrations(request.FleetId, request.CarId, cancellationToken);
                return mapper.Map<IEnumerable<RegistrationVm>>(data);
            }
        }
    }
}
