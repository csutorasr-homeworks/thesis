using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Flottapp.Application.ServiceRules;
using Flottapp.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Flottapp.Infrastructure.MongoDb.Fleet
{
    class ServiceRulesStore : AbstractStore<ServiceRule>, IServiceRulesStore
    {
        public ServiceRulesStore(IDatabaseSettings databaseSettings) : base(databaseSettings)
        {
        }

        public async Task<string> CreateMileageServiceRuleForCar(string fleetId, string carId, decimal travelledMileage, CancellationToken cancellationToken)
        {
            var serviceRule = new MileageServiceRule
            {
                CarId = carId,
                FleetId = fleetId,
                TravelledMileage = travelledMileage,
            };
            await _collection.InsertOneAsync(serviceRule, cancellationToken: cancellationToken);
            return serviceRule.Id;
        }

        public async Task<string> CreateTimeServiceRuleForCar(string fleetId, string carId, int intervalInMonth, CancellationToken cancellationToken)
        {
            var serviceRule = new TimeServiceRule
            {
                CarId = carId,
                FleetId = fleetId,
                IntervalInMonth = intervalInMonth,
            };
            await _collection.InsertOneAsync(serviceRule, cancellationToken: cancellationToken);
            return serviceRule.Id;
        }

        public async Task DeleteServiceRuleForCar(string fleetId, string carId, string serivceRuleId, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(x => x.FleetId == fleetId && x.CarId == carId && x.Id == serivceRuleId, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<ServiceRule>> GetServiceRulesForCar(string fleetId, string carId, CancellationToken cancellationToken)
        {
            var result = await _collection.FindAsync(x => x.FleetId == fleetId && x.CarId == carId, cancellationToken: cancellationToken);
            return await result.ToListAsync(cancellationToken);
        }
    }
}
