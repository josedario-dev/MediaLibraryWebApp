using MediaLibrary.WebApp.Common.Helpers;
using MediaLibrary.WebApp.Common.Helpers.Contracts;
using MediaLibrary.WebApp.Server.Data;
using MediaLibrary.WebApp.Core.Entities;
using MediaLibrary.WebApp.Server.Services.Contracts;
using MediaLibrary.WebApp.Shared;
using MediaLibrary.WebApp.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace MediaLibrary.WebApp.Server.Services
{
    public class MediaService : IMediaService
    {
        private readonly DataContext _context;

        public MediaService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<MediaDto>> GetDtoAllAsync()
        {
            var regs = await (from reg in _context.Media.Include(t=>t.Contributor) select reg).ToListAsync();

            List<MediaDto> result = new List<MediaDto>( );

            foreach (var reg in regs)
            {
                ContributorDto contributorDto = new ContributorDto()
                {
                    Biography = reg.Contributor.Biography,
                    DateOfBirth = reg.Contributor.DateOfBirth,
                    Id = reg.Contributor.Id,
                    NickName = reg.Contributor.NickName,
                    PhotoPath = reg.Contributor.PhotoPath
                };

                MediaDto regNew = new MediaDto
                {
                    Id = reg.Id,
                    Description = reg.Description,
                    FilePath = reg.FilePath,
                    MediaType = reg.MediaType,
                    PublicationDate = reg.PublicationDate,
                    ContributorId = reg.ContributorId.Value,
                    CreationDate = reg.CreationDate,
                    Title = reg.Title,
                    Contributor = contributorDto
                };
                result.Add(regNew);
            }
            return result;
        }

        public async Task<ServiceResponse<MediaDto>> GetDtoAsync(int id)
        {
            var response = new ServiceResponse<MediaDto>(data: new MediaDto(), errorMessage: string.Empty);
            try
            {
                var reg = await _context.Media.FindAsync(id);
                if (reg != null)
                {
                    response.Data = new MediaDto()
                    {
                        Id = reg.Id,
                        Description = reg.Description,
                        FilePath = reg.FilePath,
                        MediaType = reg.MediaType,
                        PublicationDate = reg.PublicationDate,
                        CreationDate = reg.CreationDate,
                        Title = reg.Title
                    };
                }
                else
                    response.ErrorMessage = "Client not found.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error occurred while retrieving data: " + ex.Message;
            }
            return response;
        }


        public async Task<int> PostAsync(MediaDto MediaDto)
        {
            var newMediaDto = new Media
            {
                Id = MediaDto.Id,
                Description = MediaDto.Description,
                FilePath = MediaDto.FilePath,
                MediaType = MediaDto.MediaType,
                PublicationDate = MediaDto.PublicationDate,
                Title = MediaDto.Title,
                CreationDate = DateTime.UtcNow,
                ContributorId = MediaDto.ContributorId
            };

            _context.Media.Add(newMediaDto);
            await _context.SaveChangesAsync();

            MediaDto.Id = newMediaDto.Id; // Set the ID of the new client

            return MediaDto.Id;
        }


        public async Task<ServiceResponse<MediaDto>> PutAsync(int id, MediaDto MediaDto)
        {
            var response = new ServiceResponse<MediaDto>(data: new MediaDto(), string.Empty);
            try
            {
                var result = await _context.Media.FindAsync(id);
                if (result != null)
                {
                    result.Title = MediaDto.Title;
                    result.Description = MediaDto.Description;
                    result.FilePath = MediaDto.FilePath;
                    result.MediaType = MediaDto.MediaType;
                    result.PublicationDate = MediaDto.PublicationDate;

                    await _context.SaveChangesAsync();
                    response.Data = MediaDto;
                }
                else
                    response.ErrorMessage = "Registro no encontrado";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ha ocurrido un error mientras se actualizaba el registro: " + ex.Message;
            }
            return response;
        }

        // Delete
        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<bool>(data: false, errorMessage: string.Empty);
            try
            {
                var reg = await _context.Media.FindAsync(id);
                if (reg != null)
                {
                    _context.Media.Remove(reg);
                    await _context.SaveChangesAsync();
                    response.Data = true;
                }
                else
                    response.ErrorMessage = "Registro no encontrado.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ha ocurrido un error mientras se eliminaba el registro: " + ex.Message;
            }
            return response;
        }
    }
}
