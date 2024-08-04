using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly IMongoCollection<SpecialOffer> _SpecialOfferCollection;
        private readonly IMapper _mapper;

        public SpecialOfferService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _SpecialOfferCollection = database.GetCollection<SpecialOffer>(databaseSettings.SpecialOfferCollectionName);
            _mapper = mapper;
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            SpecialOffer SpecialOffer = _mapper.Map<SpecialOffer>(createSpecialOfferDto);
            await _SpecialOfferCollection.InsertOneAsync(SpecialOffer);
        }

        public async Task DeleteSpecialOfferAsync(string id)
        {
            await _SpecialOfferCollection.DeleteOneAsync(SpecialOffer => SpecialOffer.Id == id);
        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            List<SpecialOffer> specialOffers = await _SpecialOfferCollection.Find(SpecialOffer => true).ToListAsync();
            List<ResultSpecialOfferDto> resultSpecialOfferDtos = _mapper.Map<List<ResultSpecialOfferDto>>(specialOffers);
            return resultSpecialOfferDtos;
        }

        public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
        {
            SpecialOffer SpecialOffer = await _SpecialOfferCollection.Find<SpecialOffer>(SpecialOffer => SpecialOffer.Id == id).FirstOrDefaultAsync();
            GetByIdSpecialOfferDto getByIdSpecialOfferDto = _mapper.Map<GetByIdSpecialOfferDto>(SpecialOffer);
            return getByIdSpecialOfferDto;
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            SpecialOffer SpecialOffer = _mapper.Map<SpecialOffer>(updateSpecialOfferDto);
            await _SpecialOfferCollection.FindOneAndReplaceAsync(c => c.Id == updateSpecialOfferDto.Id, SpecialOffer);
        }
    }
}
