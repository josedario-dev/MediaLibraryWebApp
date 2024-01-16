using MediaLibrary.WebApp.Shared.DTOs;
using MediaLibrary.WebApp.Shared.Overviews;
using MediaLibrary.WebApp.Core;
using MediaLibrary.WebApp.Core.Enums;

namespace MediaLibrary.WebApp.Server.Services.Contracts
{
    public interface IContributorDetailService
    {
        Task<List<ContributorDto>> GetDtoAllAsync(string userId, UserType userType);
    }
}
