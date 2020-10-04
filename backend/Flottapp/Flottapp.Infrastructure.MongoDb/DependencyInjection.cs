using Flottapp.Application.Car;
using Flottapp.Application.Fleet;
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
            return services;
        }

        private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton((System.Func<System.IServiceProvider, IDatabaseSettings>)(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value));
        }
    }
}
