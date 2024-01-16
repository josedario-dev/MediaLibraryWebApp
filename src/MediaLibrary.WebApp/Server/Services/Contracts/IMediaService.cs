using MediaLibrary.WebApp.Common.Helpers;
using MediaLibrary.WebApp.Shared.DTOs;

namespace MediaLibrary.WebApp.Server.Services.Contracts
{
    public interface IMediaService
    {
        Task<List<MediaDto>> GetDtoAllAsync();
        Task<ServiceResponse<MediaDto>> GetDtoAsync(int id);
        Task<int> PostAsync(MediaDto MediaDto);
        Task<ServiceResponse<MediaDto>> PutAsync(int id, MediaDto MediaDto);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
