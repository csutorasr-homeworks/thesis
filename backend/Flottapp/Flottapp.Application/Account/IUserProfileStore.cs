using Flottapp.Domain;
using Flottapp.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Application.Account
{
    public interface IUserProfileStore
    {
        Task SetProfile(UserProfile userProfile, CancellationToken cancellationToken);
        Task<UserProfile> GetProfile(AuthorizationData authorizationData, CancellationToken cancellationToken);
    }
}