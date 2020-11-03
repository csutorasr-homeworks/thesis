using Flottapp.Application.Account;
using Flottapp.Application.Car;
using Flottapp.Application.Fleet;
using Flottapp.Application.MonthlyAggregate;
using Flottapp.Application.Payments;
using Flottapp.Application.Registration;
using Flottapp.Application.ServiceOccasions;
using Flottapp.Application.ServiceRules;
using Flottapp.Domain;
using Flottapp.Infrastructure.MongoDb;
using Flottapp.Infrastructure.MongoDb.Fleet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, Configuration.IConfiguration configuration)
        {
            ConfigureDatabase(services, configuration);
            services.AddSingleton<IFleetStore, FleetStore>();
            services.AddSingleton<ICarsStore, FleetStore>();
            services.AddSingleton<IRegistrationsStore, RegistrationsStore>();
            services.AddSingleton<IMonthlyAggregatesStore, MonthlyAggregatesStore>();
            services.AddSingleton<IMonthlyAggregateLimitsStore, MonthlyAggregateLimitsStore>();
            services.AddSingleton<IPaymentsStore, PaymentsStore>();
            services.AddSingleton<IServiceRulesStore, ServiceRulesStore>();
            services.AddSingleton<IServiceOccasionsStore, ServiceOccasionsStore>();
            services.AddSingleton<IUserProfileStore, UserProfileStore>();
            BsonClassMap.RegisterClassMap<Fleet>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
            });
            BsonClassMap.RegisterClassMap<Car>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetIgnoreIfDefault(true);
            });
            BsonClassMap.RegisterClassMap<MonthlyAggregateLimit>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetIgnoreIfDefault(true);
            });
            BsonClassMap.RegisterClassMap<Payment>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetIgnoreIfDefault(true);
            });
            BsonClassMap.RegisterClassMap<ServiceRule>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetIgnoreIfDefault(true);
                cm.AddKnownType(typeof(TimeServiceRule));
                cm.AddKnownType(typeof(MileageServiceRule));
                cm.SetIsRootClass(true);
            });
            BsonClassMap.RegisterClassMap<ServiceOccasion>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetIgnoreIfDefault(true);
            });
            BsonClassMap.RegisterClassMap<UserProfile>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(x => x.Id).SetIdGenerator(StringObjectIdGenerator.Instance).SetIgnoreIfDefault(true);
            });
            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton((System.Func<System.IServiceProvider, IDatabaseSettings>)(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value));
        }
    }
}
