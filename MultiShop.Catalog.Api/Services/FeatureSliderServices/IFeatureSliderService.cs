using MultiShop.Catalog.Api.Dtos.FutureSliderDtos;

namespace MultiShop.Catalog.Api.Services.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync();
        Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto);
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto);
        Task DeleteFeatureSliderAsync(string id);
        Task FeatureSliderChangeStatusToTrue(string id);
        Task FeatureSliderChangeStatusToFalse(string id);
        Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id);
    }
}
