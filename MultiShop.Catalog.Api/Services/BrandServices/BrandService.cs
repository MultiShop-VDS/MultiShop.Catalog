using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.BrandsDto;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IMongoCollection<Brand> _BrandCollection;

        public BrandService(IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _BrandCollection = database.GetCollection<Brand>(databaseSettings.BrandCollectionName);
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            var brand = new Brand()
            {
                ImageName = createBrandDto.ImageName,
                Name = createBrandDto.Name
            };
            await _BrandCollection.InsertOneAsync(brand);
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _BrandCollection.DeleteOneAsync(Brand => Brand.Id == id);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            List<Brand> categories = await _BrandCollection.Find(Brand => true).ToListAsync();
            List<ResultBrandDto> resultBrandDtos = categories.Select(c => new ResultBrandDto()
            {
                Id = c.Id,
                ImageName = c.ImageName,
                Name = c.Name
            }).ToList();
            return resultBrandDtos;
        }

        public async Task<GetByIdBrandDto> GetByIdBrandAsync(string id)
        {
            Brand Brand = await _BrandCollection.Find<Brand>(Brand => Brand.Id == id).FirstOrDefaultAsync();
            GetByIdBrandDto getByIdBrandDto = new GetByIdBrandDto()
            {
                Id = Brand.Id,
                ImageName = Brand.ImageName,
                Name = Brand.Name
            };
            return getByIdBrandDto;
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            Brand Brand = new Brand() { Id = updateBrandDto.Id, ImageName = updateBrandDto.ImageName, Name = updateBrandDto.Name };
            await _BrandCollection.FindOneAndReplaceAsync(c => c.Id == updateBrandDto.Id, Brand);
        }
    }
}
