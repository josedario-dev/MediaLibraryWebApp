using MediaLibrary.WebApp.Common.Helpers;
using MediaLibrary.WebApp.Shared.DTOs;

namespace MediaLibrary.WebApp.Server.Services.Contracts
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetDtoAllAsync();
        Task<ServiceResponse<CountryDto>> GetDtoAsync(int id);
        Task<ServiceResponse<CountryDto>> Post(CountryDto CountryDto);
        Task<ServiceResponse<CountryDto>> Put(int id, CountryDto CountryDto);
        Task<ServiceResponse<bool>> Delete(int id);
    }
}
