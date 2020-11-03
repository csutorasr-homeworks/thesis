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
    public class AddUserToFleetCommand : IRequest
    {
        public string Id { get; set; }
        public Dto Data { get; set; }
        public class Handler : IRequestHandler<AddUserToFleetCommand>
        {
            private readonly IFleetStore fleetStore;
            private readonly IUserProfileStore userProfileStore;

            public Handler(IFleetStore fleetStore, IUserProfileStore userProfileStore)
            {
                this.fleetStore = fleetStore;
                this.userProfileStore = userProfileStore;
            }
            public async Task<Unit> Handle(AddUserToFleetCommand request, CancellationToken cancellationToken)
            {
                var authorizationData = await userProfileStore.GetAuthorizationDataByName(request.Data.UserId, cancellationToken);
                await fleetStore.AddUserToFleet(request.Id, authorizationData, cancellationToken);
                return Unit.Value;
            }
        }

        public class Dto
        {
            public string UserId { get; set; }
        }
    }
}
