using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.OfferDiscountServices
{
    public class OfferDiscountService : IOfferDiscountService
    {
        private readonly IMongoCollection<OfferDiscount> _OfferDiscountCollection;
        private readonly IMapper _mapper;

        public OfferDiscountService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _OfferDiscountCollection = database.GetCollection<OfferDiscount>(databaseSettings.OfferDiscountCollectionName);
            _mapper = mapper;
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
        {
            OfferDiscount OfferDiscount = _mapper.Map<OfferDiscount>(createOfferDiscountDto);
            await _OfferDiscountCollection.InsertOneAsync(OfferDiscount);
        }

        public async Task DeleteOfferDiscountAsync(string id)
        {
            await _OfferDiscountCollection.DeleteOneAsync(OfferDiscount => OfferDiscount.Id == id);
        }

        public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync()
        {
            List<OfferDiscount> categories = await _OfferDiscountCollection.Find(OfferDiscount => true).ToListAsync();
            List<ResultOfferDiscountDto> resultOfferDiscountDtos = _mapper.Map<List<ResultOfferDiscountDto>>(categories);
            return resultOfferDiscountDtos;
        }

        public async Task<GetByIdOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
        {
            OfferDiscount OfferDiscount = await _OfferDiscountCollection.Find<OfferDiscount>(OfferDiscount => OfferDiscount.Id == id).FirstOrDefaultAsync();
            GetByIdOfferDiscountDto getByIdOfferDiscountDto = _mapper.Map<GetByIdOfferDiscountDto>(OfferDiscount);
            return getByIdOfferDiscountDto;
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            OfferDiscount OfferDiscount = _mapper.Map<OfferDiscount>(updateOfferDiscountDto);
            await _OfferDiscountCollection.FindOneAndReplaceAsync(c => c.Id == updateOfferDiscountDto.Id, OfferDiscount);
        }
    }
}
