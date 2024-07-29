using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.ProductDtos;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            Product product = _mapper.Map<Product>(createProductDto);
            await _productCollection.InsertOneAsync(product);
        }

        public async Task DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(product => product.Id == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            List<Product> products = await _productCollection.Find<Product>(product => true).ToListAsync();
            return _mapper.Map<List<ResultProductDto>>(products);
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            Product product = await _productCollection.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(product);
        }

        public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var values = await (await _productCollection.FindAsync(x => true)).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await (await _categoryCollection.FindAsync<Category>(x => x.Id == item.CategoryId)).FirstAsync();
            }
            return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            await _productCollection.FindOneAndReplaceAsync(product => product.Id == updateProductDto.Id, _mapper.Map<Product>(updateProductDto));
        }
    }
}
