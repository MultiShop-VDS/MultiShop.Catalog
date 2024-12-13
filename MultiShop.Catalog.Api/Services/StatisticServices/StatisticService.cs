using MongoDB.Bson;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMongoCollection<Brand> _brandCollection;

        public StatisticService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _brandCollection = database.GetCollection<Brand>(_databaseSettings.BrandCollectionName);
        }
        public async Task<long> GetBrandCount()
        {
            return await _brandCollection.CountDocumentsAsync(FilterDefinition<Brand>.Empty);
        }
        public Task<long> GetCategoryCount()
        {
            return _categoryCollection.CountDocumentsAsync(FilterDefinition<Category>.Empty);
        }

        public async Task<decimal> GetProductAvgPrice()
        {
            List<Product> products = await _productCollection.Find<Product>(product => true).ToListAsync();

            var avgPrice = products.Aggregate(
                new { Count = 0, AvgPrice = 0m },
                (acc, p) => new { Count = acc.Count + 1, AvgPrice = acc.AvgPrice + p.Price },
                acc => new { Count = acc.Count, AvgPrice = acc.Count > 0 ? acc.AvgPrice / acc.Count : 0m }
            );

            return avgPrice?.AvgPrice ?? 0m;
        }

        public async Task<string> GetMaxPriceProductName()
        {
            List<Product> products = await _productCollection.Find<Product>(product => true).ToListAsync();

            var maxPriceProduct = products.OrderByDescending(p => p.Price).FirstOrDefault();

            return maxPriceProduct?.Name ?? "No Product";
        }

        public async Task<string> GetMinPriceProductName()
        {
            List<Product> products = await _productCollection.Find<Product>(product => true).ToListAsync();

            var minPriceProduct = products.OrderBy(p => p.Price).FirstOrDefault();

            return minPriceProduct?.Name ?? "No Product";
        }

        public Task<long> GetProductCount()
        {
            return _productCollection.CountDocumentsAsync(FilterDefinition<Product>.Empty);
        }
    }
}
