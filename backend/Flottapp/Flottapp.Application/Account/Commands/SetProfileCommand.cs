using Flottapp.Domain;
using Flottapp.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.Account
{
    public class SetProfileCommand : IRequest, IAuthorizableRequest
    {
        public AuthorizationData AuthorizationData { get; set; }
        public Dto Data { get; set; }
        class SetProfileCommandHandler : IRequestHandler<SetProfileCommand>
        {
            private readonly IUserProfileStore accountStore;

            public SetProfileCommandHandler(IUserProfileStore accountStore)
            {
                this.accountStore = accountStore;
            }
            public async Task<Unit> Handle(SetProfileCommand request, CancellationToken cancellationToken)
            {
                await accountStore.SetProfile(new UserProfile
                {
                    AuthorizationData = request.AuthorizationData,
                    Name = request.Data.Name,
                }, cancellationToken);
                return Unit.Value;
            }
        }
        public class Dto
        {
            public string Name { get; set; }
        }
    }
}
