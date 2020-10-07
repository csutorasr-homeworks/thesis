using Flottapp.Application.ServiceOccasions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class DeleteServiceOccasionForCarCommand : IRequest
    {
        public string FleetId { get; set; }
        public string CarId { get; set; }
        public string OccasionId { get; set; }
        public class Handler : IRequestHandler<DeleteServiceOccasionForCarCommand>
        {
            private readonly IServiceOccasionsStore serviceOccasionsStore;

            public Handler(IServiceOccasionsStore serviceOccasionsStore)
            {
                this.serviceOccasionsStore = serviceOccasionsStore;
            }
            public async Task<Unit> Handle(DeleteServiceOccasionForCarCommand request, CancellationToken cancellationToken)
            {
                await serviceOccasionsStore.DeleteServiceOccasionForCar(request.FleetId, request.CarId, request.OccasionId, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
