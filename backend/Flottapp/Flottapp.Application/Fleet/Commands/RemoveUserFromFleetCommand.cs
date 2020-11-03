using Flottapp.Application.Account;
using Flottapp.Application.Fleet;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Commands
{
    public class RemoveUserFromFleetCommand : IRequest
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public class Handler : IRequestHandler<RemoveUserFromFleetCommand>
        {
            private readonly IFleetStore fleetStore;
            private readonly IUserProfileStore userProfileStore;

            public Handler(IFleetStore fleetStore, IUserProfileStore userProfileStore)
            {
                this.fleetStore = fleetStore;
                this.userProfileStore = userProfileStore;
            }
            public async Task<Unit> Handle(RemoveUserFromFleetCommand request, CancellationToken cancellationToken)
            {
                var authorizationData = await userProfileStore.GetAuthorizationDataByName(request.UserId, cancellationToken);
                await fleetStore.RemoveUserFromFleet(request.Id, authorizationData, cancellationToken);
                return Unit.Value;
            }
        }
    }
}
