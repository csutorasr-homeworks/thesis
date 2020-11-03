using AutoMapper;
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
            private readonly IMapper mapper;
            private readonly IMediator mediator;

            public SetProfileCommandHandler(IUserProfileStore accountStore, IMapper mapper, IMediator mediator)
            {
                this.accountStore = accountStore;
                this.mapper = mapper;
                this.mediator = mediator;
            }
            public async Task<Unit> Handle(SetProfileCommand request, CancellationToken cancellationToken)
            {
                var userProfile = new UserProfile
                {
                    AuthorizationData = request.AuthorizationData,
                    Name = request.Data.Name,
                };
                await accountStore.SetProfile(userProfile, cancellationToken);
                var @event = mapper.Map<UserProfileChangedEvent>(userProfile);
                await mediator.Publish(@event, cancellationToken: cancellationToken);
                return Unit.Value;
            }
        }
        public class Dto
        {
            public string Name { get; set; }
        }
    }
}
