using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flottapp.Infrastructure.MongoDb
{
    abstract class AbstractStore<T>
    {
        protected IMongoCollection<T> _collection;

        public AbstractStore(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }
    }
}
