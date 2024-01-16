using MediaLibrary.WebApp.Common.Helpers;
using MediaLibrary.WebApp.Shared.DTOs;

namespace MediaLibrary.WebApp.Server.Services.Contracts
{
    public interface IContributorService
    {
        Task<List<ContributorDto>> GetDtoAllAsync();
        Task<ServiceResponse<ContributorDto>> GetDtoAsync(int id);
        Task<int> PostAsync(ContributorDto contributorDto);
        Task<ServiceResponse<ContributorDto>> PutAsync(int id, ContributorDto contributorDto);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
