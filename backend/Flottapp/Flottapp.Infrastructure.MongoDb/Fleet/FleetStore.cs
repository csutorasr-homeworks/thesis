using Flottapp.Application.Fleet;
using Flottapp.Application.Fleet.Exceptions;
using Flottapp.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Flottapp.Infrastructure.MongoDb.Fleet
{
    class FleetStore : AbstractStore<Domain.Fleet>, IFleetStore
    {
        public FleetStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task<string> AddCarToFleet(string fleetId, Car car, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(x => x.Id == fleetId,
                                                    Builders<Domain.Fleet>.Update.AddToSet(x => x.Cars, car),
                                                    cancellationToken: cancellationToken);
            if (result.MatchedCount == 0)
            {
                throw new FleetNotFoundException();
            }
            return car.Id;
        }

        public async Task AddUserToFleet(string id, string userId, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(x => x.Id == id,
                                                    Builders<Domain.Fleet>.Update.AddToSet(x => x.Users, userId),
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

        public async Task<string> CreateFleet(string name, CancellationToken cancellationToken)
        {
            var fleet = new Domain.Fleet
            {
                Name = name
            };
            await _collection.InsertOneAsync(fleet, cancellationToken: cancellationToken);
            return fleet.Id;
        }

        public async Task DeactivateCarForFleet(string fleetId, string carId, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(x => x.Id == fleetId, Builders<Domain.Fleet>.Update.Set("Cars.$[elem].Activated", false), new UpdateOptions { ArrayFilters = new[] { new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("elem._id", carId)) } }, cancellationToken);
        }

        public async Task DeleteFleet(string id, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id, cancellationToken: cancellationToken);
            if (result.DeletedCount == 0)
            {
                throw new FleetNotFoundException();
            }
        }

        public async Task<Car> GetCarForFleet(string fleetId, string carId, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(x => x.Id == fleetId && x.Cars.Any(y => y.Id == carId), cancellationToken: cancellationToken);
            var fleet = await result.FirstOrDefaultAsync() ?? throw new FleetNotFoundException();
            return fleet.Cars.FirstOrDefault(x => x.Id == carId);
        }

        public async Task<Domain.Fleet> GetFleet(string id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(x => x.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync() ?? throw new FleetNotFoundException();
        }

        public async Task<List<Domain.Fleet>> GetFleets(CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(Builders<Domain.Fleet>.Filter.Empty, cancellationToken: cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }

        public async Task RemoveUserFromFleet(string id, string userId, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(x => x.Id == id,
                                                    Builders<Domain.Fleet>.Update.Pull(x => x.Users, userId),
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

        public async Task SaveName(Domain.Fleet fleet, CancellationToken cancellationToken)
        {
            var result = await _collection.UpdateOneAsync(x => x.Id == fleet.Id,
                                                    Builders<Domain.Fleet>.Update.Set(x => x.Name, fleet.Name),
                                                    cancellationToken: cancellationToken);
            if (result.MatchedCount == 0)
            {
                throw new FleetNotFoundException();
            }
        }
    }
}
