using AutoMapper;
using Flottapp.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.Account
{
    public class GetProfileQuery : IRequest<ProfileVm>, IAuthorizableRequest
    {
        public AuthorizationData AuthorizationData { get; set; }
        class SetProfileCommandHandler : IRequestHandler<GetProfileQuery, ProfileVm>
        {
            private readonly IUserProfileStore accountStore;
            private readonly IMapper mapper;

            public SetProfileCommandHandler(IUserProfileStore accountStore, IMapper mapper)
            {
                this.accountStore = accountStore;
                this.mapper = mapper;
            }
            public async Task<ProfileVm> Handle(GetProfileQuery request, CancellationToken cancellationToken)
            {
                var profile = await accountStore.GetProfile(request.AuthorizationData, cancellationToken);
                return mapper.Map<ProfileVm>(profile);
            }
        }
    }
}
