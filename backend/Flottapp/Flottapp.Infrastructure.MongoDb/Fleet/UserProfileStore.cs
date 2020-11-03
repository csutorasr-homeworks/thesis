using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Application.Account;
using Flottapp.Application.Account.Exceptions;
using Flottapp.Domain;
using Flottapp.Model;
using MongoDB.Driver;

namespace Flottapp.Infrastructure.MongoDb.Fleet
{
    class UserProfileStore : AbstractStore<UserProfile>, IUserProfileStore
    {
        public UserProfileStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task<UserProfile> GetProfile(AuthorizationData authorizationData, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(x => x.AuthorizationData.Authority == authorizationData.Authority && x.AuthorizationData.Id == authorizationData.Id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync() ?? throw new UserProfileNotFoundException();
        }

        public async Task SetProfile(UserProfile userProfile, CancellationToken cancellationToken)
        {
            await _collection.UpdateOneAsync(x => x.AuthorizationData.Authority == userProfile.AuthorizationData.Authority && x.AuthorizationData.Id == userProfile.AuthorizationData.Id,
                Builders<UserProfile>.Update.Set(x => x.Name, userProfile.Name)
                                            .SetOnInsert(x => x.AuthorizationData.Authority, userProfile.AuthorizationData.Authority)
                                            .SetOnInsert(x => x.AuthorizationData.Id, userProfile.AuthorizationData.Id),
                new UpdateOptions
                {
                    IsUpsert = true,
                }, cancellationToken: cancellationToken);
        }
    }
}
