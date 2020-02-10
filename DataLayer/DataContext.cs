using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Microsoft.Extensions.Options;

namespace DataLayer
{
    public class DataContext : IDataContext
    {
        public readonly IMongoDatabase mongoDatabase;

        public DataContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            mongoDatabase = client.GetDatabase(options.Value.Database);

        }

        IMongoCollection<City> IDataContext.Cities => mongoDatabase.GetCollection<City>("Cities");
        IMongoCollection<Factory> IDataContext.Factories => mongoDatabase.GetCollection<Factory>("Factories");
        IMongoCollection<Worker> IDataContext.Workers => mongoDatabase.GetCollection<Worker>("Workers");
    }

    public interface IDataContext
    {
        IMongoCollection<City> Cities { get;  }
        IMongoCollection<Factory> Factories { get; }
        IMongoCollection<Worker> Workers { get; }
    }
}
