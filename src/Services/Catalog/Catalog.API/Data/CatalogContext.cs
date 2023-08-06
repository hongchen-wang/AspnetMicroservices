using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; set; }

        public CatalogContext(IConfiguration configuration) 
        {
            var connectionString = configuration.GetValue<string>("DatabaseSetting:ConnectionString");
            var client = new MongoClient(connectionString);

            var databaseSetting = configuration.GetValue<string>("DatabaseSetting:DatabaseName");
            var database = client.GetDatabase(databaseSetting);

            var collectionName = configuration.GetValue<string>("DatabaseSetting:CollectionName");
            Products = database.GetCollection<Product>(collectionName);

            CatalogContextSeed.SeedData(Products);
        }
    }
}
