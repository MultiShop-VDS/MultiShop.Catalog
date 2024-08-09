using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.ProducImageDtos;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _ProductImageCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _ProductImageCollection = database.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            ProductImage productImage = _mapper.Map<ProductImage>(createProductImageDto);
            await _ProductImageCollection.InsertOneAsync(productImage);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _ProductImageCollection.DeleteOneAsync(ProductImage => ProductImage.Id == id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            List<ProductImage> categories = await _ProductImageCollection.Find(ProductImage => true).ToListAsync();
            List<ResultProductImageDto> resultProductImageDtos = _mapper.Map<List<ResultProductImageDto>>(categories);
            return resultProductImageDtos;
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            ProductImage productImage = await _ProductImageCollection.Find<ProductImage>(ProductImage => ProductImage.Id == id).FirstOrDefaultAsync();
            GetByIdProductImageDto getByIdProductImageDto = _mapper.Map<GetByIdProductImageDto>(productImage);
            return getByIdProductImageDto;
        }

        public async Task<GetByIdProductImageDto> GetByProductIdProductImagesAsync(string id)
        {
            var values = await (await _ProductImageCollection.FindAsync<ProductImage>(ProductImage => ProductImage.ProductId == id)).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            ProductImage ProductImage = _mapper.Map<ProductImage>(updateProductImageDto);
            await _ProductImageCollection.FindOneAndReplaceAsync(c => c.Id == updateProductImageDto.Id, ProductImage);
        }
    }
}
