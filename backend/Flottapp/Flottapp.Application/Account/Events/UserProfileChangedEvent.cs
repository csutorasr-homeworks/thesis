using Flottapp.Model;
using MediatR;

namespace Flottapp.Application.Account
{
    public class UserProfileChangedEvent : INotification
    {
        public AuthorizationData AuthorizationData { get; set; }
        public string Name { get; set; }
    }
}
