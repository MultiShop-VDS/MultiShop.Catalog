using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.AboutDtos;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly IMongoCollection<About> _AboutCollection;
        private readonly IMapper _mapper;

        public AboutService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _AboutCollection = database.GetCollection<About>(databaseSettings.AboutCollectionName);
            _mapper = mapper;
        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            About About = _mapper.Map<About>(createAboutDto);
            await _AboutCollection.InsertOneAsync(About);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _AboutCollection.DeleteOneAsync(About => About.Id == id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            List<About> Abouts = await _AboutCollection.Find(About => true).ToListAsync();
            List<ResultAboutDto> resultAboutDtos = _mapper.Map<List<ResultAboutDto>>(Abouts);
            return resultAboutDtos;
        }

        public async Task<GetByIdAboutDto> GetByIdAboutAsync(string id)
        {
            About About = await _AboutCollection.Find<About>(About => About.Id == id).FirstOrDefaultAsync();
            GetByIdAboutDto getByIdAboutDto = _mapper.Map<GetByIdAboutDto>(About);
            return getByIdAboutDto;
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            About About = _mapper.Map<About>(updateAboutDto);
            await _AboutCollection.FindOneAndReplaceAsync(c => c.Id == updateAboutDto.Id, About);
        }
    }
}
