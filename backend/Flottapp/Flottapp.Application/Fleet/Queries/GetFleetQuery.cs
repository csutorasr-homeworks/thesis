using AutoMapper;
using Flottapp.Application.Account;
using Flottapp.Application.Fleet;
using Flottapp.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastucture.Queries
{
    public class GetFleetQuery : IRequest<FleetVm>, IAuthorizableRequest
    {
        public string Id { get; set; }
        public AuthorizationData AuthorizationData { get; set; }

        public class Handler : IRequestHandler<GetFleetQuery, FleetVm>
        {
            private readonly IFleetStore fleetStore;
            private readonly IMapper mapper;
            private readonly IUserProfileStore userProfileStore;

            public Handler(IFleetStore fleetStore, IMapper mapper, IUserProfileStore userProfileStore)
            {
                this.fleetStore = fleetStore;
                this.mapper = mapper;
                this.userProfileStore = userProfileStore;
            }
            public async Task<FleetVm> Handle(GetFleetQuery request, CancellationToken cancellationToken)
            {
                var data = await fleetStore.GetFleet(request.Id, request.AuthorizationData, cancellationToken);
                var viewModel = mapper.Map<FleetVm>(data);
                viewModel.Users = await userProfileStore.ListNameByAuthorizationData(data.Users, cancellationToken);
                return viewModel;
            }
        }
    }
}
