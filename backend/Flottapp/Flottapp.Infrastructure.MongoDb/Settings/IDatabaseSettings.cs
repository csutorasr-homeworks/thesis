using System;

namespace Flottapp.Infrastructure.MongoDb
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
