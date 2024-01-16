using MediaLibrary.WebApp.Shared.DTOs;
using MediaLibrary.WebApp.Shared.Overviews;

namespace MediaLibrary.WebApp.Server.Services.Contracts
{
    public interface IContributorDetailService
    {
        Task<List<ContributorDto>> GetDtoAllAsync(string userId);
    }
}
