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
        private readonly IMapper _mapper;

        public BrandService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _BrandCollection = database.GetCollection<Brand>(databaseSettings.BrandCollectionName);
            _mapper = mapper;
        }

        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            Brand Brand = _mapper.Map<Brand>(createBrandDto);
            await _BrandCollection.InsertOneAsync(Brand);
        }

        public async Task DeleteBrandAsync(string id)
        {
            await _BrandCollection.DeleteOneAsync(Brand => Brand.Id == id);
        }

        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            List<Brand> categories = await _BrandCollection.Find(Brand => true).ToListAsync();
            List<ResultBrandDto> resultBrandDtos = _mapper.Map<List<ResultBrandDto>>(categories);
            return resultBrandDtos;
        }

        public async Task<GetByIdBrandDto> GetByIdBrandAsync(string id)
        {
            Brand Brand = await _BrandCollection.Find<Brand>(Brand => Brand.Id == id).FirstOrDefaultAsync();
            GetByIdBrandDto getByIdBrandDto = _mapper.Map<GetByIdBrandDto>(Brand);
            return getByIdBrandDto;
        }

        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            Brand Brand = _mapper.Map<Brand>(updateBrandDto);
            await _BrandCollection.FindOneAndReplaceAsync(c => c.Id == updateBrandDto.Id, Brand);
        }
    }
}
