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
            private readonly IMediator mediator;

            public Handler(IRegistrationsStore registrationStore, IMapper mapper, IMediator mediator)
            {
                this.registrationStore = registrationStore;
                this.mapper = mapper;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(DeleteRegistrationForCarCommand request, CancellationToken cancellationToken)
            {
                await registrationStore.DeleteRegistrationForCar(request.FleetId, request.CarId, request.RegistrationId, cancellationToken);
                await mediator.Publish(mapper.Map<DeleteRegistrationForCarEvent>(request));
                return Unit.Value;
            }
        }
    }
}
