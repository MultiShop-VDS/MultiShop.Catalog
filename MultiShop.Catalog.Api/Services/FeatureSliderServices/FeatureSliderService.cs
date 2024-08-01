using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.FutureSliderDtos;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> _featureSliderCollection;
        private readonly IMapper _mapper;

        public FeatureSliderService(IDatabaseSettings settings, IMapper mapper)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _featureSliderCollection = database.GetCollection<FeatureSlider>(settings.FeatureSliderCollectionName);
            _mapper = mapper;
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            FeatureSlider featureSlider = _mapper.Map<FeatureSlider>(createFeatureSliderDto);
            await _featureSliderCollection.InsertOneAsync(featureSlider);
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
            await _featureSliderCollection.DeleteOneAsync(featureSlider => featureSlider.Id == id);
        }

        public Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            List<FeatureSlider> featureSliders = await _featureSliderCollection.Find(featureSlider => true).ToListAsync();
            List<ResultFeatureSliderDto> resultFeatureSliderDtos = _mapper.Map<List<ResultFeatureSliderDto>>(featureSliders);
            return resultFeatureSliderDtos;
        }

        public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
        {
            FeatureSlider featureSlider = await _featureSliderCollection.Find<FeatureSlider>(featureSlider => featureSlider.Id == id).FirstOrDefaultAsync();
            GetByIdFeatureSliderDto getByIdFeatureSliderDto = _mapper.Map<GetByIdFeatureSliderDto>(featureSlider);
            return getByIdFeatureSliderDto;
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            FeatureSlider featureSlider = _mapper.Map<FeatureSlider>(updateFeatureSliderDto);
            await _featureSliderCollection.FindOneAndReplaceAsync(c => c.Id == updateFeatureSliderDto.Id, featureSlider);
        }
    }
}
