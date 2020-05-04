using Flottapp.Application.Fleet;
using Flottapp.Application.Fleet.Exceptions;
using Flottapp.Domain;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastructure.MongoDb.Car
{
    class FleetStore : AbstractStore<Fleet>, IFleetStore
    {
        public FleetStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task AddUserToFleet(string id, string userId, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(x => x.Id == id,
                                                    Builders<Fleet>.Update.AddToSet(x => x.Users, userId),
                                                    cancellationToken: cancellationToken);
            if (result.MatchedCount == 0)
            {
                throw new FleetNotFoundException();
            }
            if (result.ModifiedCount == 0)
            {
                throw new FleetUserAlreadyAddedException();
            }
        }

        public Task ChangeName(string id, string name, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> CreateFleet(string name, CancellationToken cancellationToken)
        {
            var fleet = new Fleet
            {
                Name = name
            };
            await _collection.InsertOneAsync(fleet, cancellationToken: cancellationToken);
            return fleet.Id;
        }

        public async Task DeleteFleet(string id, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (result.DeletedCount == 0)
            {
                throw new FleetNotFoundException();
            }
        }

        public async Task<Fleet> GetFleet(string id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(x => x.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync() ?? throw new FleetNotFoundException();
        }

        public async Task<List<Fleet>> GetFleets(CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(Builders<Fleet>.Filter.Empty, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }

        public async Task RemoveUserFromFleet(string id, string userId, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(x => x.Id == id,
                                                    Builders<Fleet>.Update.Pull(x => x.Users, userId),
                                                    cancellationToken: cancellationToken);
            if (result.MatchedCount == 0)
            {
                throw new FleetNotFoundException();
            }
            if (result.ModifiedCount == 0)
            {
                throw new FleetUserNotFoundException();
            }
        }

        public async Task SaveName(Fleet fleet, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(x => x.Id == fleet.Id,
                                                    Builders<Fleet>.Update.Set(x => x.Name, fleet.Name),
                                                    cancellationToken: cancellationToken);
            if (result.MatchedCount == 0)
            {
                throw new FleetNotFoundException();
            }
        }
    }
}
