using AutoMapper;
using Flottapp.Application.Registration;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteRegistrationForCarCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public string RegistrationId { get; set; }
        public class Handler : IRequestHandler<DeleteRegistrationForCarCommand>
        {
            private readonly IRegistrationsStore registrationStore;
            private readonly IMapper mapper;

            public Handler(IRegistrationsStore registrationStore, IMapper mapper)
            {
                this.registrationStore = registrationStore;
                this.mapper = mapper;
            }
            public async Task<Unit> Handle(DeleteRegistrationForCarCommand request, CancellationToken cancellationToken)
            {
                await registrationStore.DeleteRegistrationForCar(request.FleetId, request.CarId, request.RegistrationId, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
