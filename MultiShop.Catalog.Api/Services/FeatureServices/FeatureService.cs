using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.FeatureDtos;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly IMongoCollection<Feature> _FeatureCollection;
        private readonly IMapper _mapper;

        public FeatureService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _FeatureCollection = database.GetCollection<Feature>(databaseSettings.FeatureCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
        {
            Feature Feature = _mapper.Map<Feature>(createFeatureDto);
            await _FeatureCollection.InsertOneAsync(Feature);
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await _FeatureCollection.DeleteOneAsync(Feature => Feature.Id == id);
        }

        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
        {
            List<Feature> categories = await _FeatureCollection.Find(Feature => true).ToListAsync();
            List<ResultFeatureDto> resultFeatureDtos = _mapper.Map<List<ResultFeatureDto>>(categories);
            return resultFeatureDtos;
        }

        public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id)
        {
            Feature Feature = await _FeatureCollection.Find<Feature>(Feature => Feature.Id == id).FirstOrDefaultAsync();
            GetByIdFeatureDto getByIdFeatureDto = _mapper.Map<GetByIdFeatureDto>(Feature);
            return getByIdFeatureDto;
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            Feature Feature = _mapper.Map<Feature>(updateFeatureDto);
            await _FeatureCollection.FindOneAndReplaceAsync(c => c.Id == updateFeatureDto.Id, Feature);
        }
    }
}
