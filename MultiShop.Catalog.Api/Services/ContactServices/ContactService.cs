using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Api.Dtos.ContactDtos;
using MultiShop.Catalog.Api.Entities;
using MultiShop.Catalog.Api.Settings;

namespace MultiShop.Catalog.Api.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _ContactCollection;
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            MongoClient client = new MongoClient(databaseSettings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(databaseSettings.DatabaseName);
            _ContactCollection = database.GetCollection<Contact>(databaseSettings.ContactCollectionName);
            _mapper = mapper;
        }

        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            Contact Contact = _mapper.Map<Contact>(createContactDto);
            await _ContactCollection.InsertOneAsync(Contact);
        }

        public async Task DeleteContactAsync(string id)
        {
            await _ContactCollection.DeleteOneAsync(Contact => Contact.Id == id);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            List<Contact> Contacts = await _ContactCollection.Find(Contact => true).ToListAsync();
            List<ResultContactDto> resultContactDtos = _mapper.Map<List<ResultContactDto>>(Contacts);
            return resultContactDtos;
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            Contact Contact = await _ContactCollection.Find<Contact>(Contact => Contact.Id == id).FirstOrDefaultAsync();
            GetByIdContactDto getByIdContactDto = _mapper.Map<GetByIdContactDto>(Contact);
            return getByIdContactDto;
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            Contact Contact = _mapper.Map<Contact>(updateContactDto);
            await _ContactCollection.FindOneAndReplaceAsync(c => c.Id == updateContactDto.Id, Contact);
        }
    }
}
