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
    public class ContributorService : IContributorService
    {
        private readonly DataContext _context;

        public ContributorService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ContributorDto>> GetDtoAllAsync()
        {
            var regs = await (from reg in _context.Contributor select reg).ToListAsync();
            List<ContributorDto> result = new List<ContributorDto>();
            foreach (var reg in regs)
            {
                ContributorDto regNew = new ContributorDto
                {
                    Id = reg.Id,
                    Biography = reg.Biography,
                    DateOfBirth = reg.DateOfBirth,
                    NickName = reg.NickName,
                    PhotoPath = reg.PhotoPath
                };
                result.Add(regNew);
            }
            return result;
        }

        public async Task<ServiceResponse<ContributorDto>> GetDtoAsync(int id)
        {
            var response = new ServiceResponse<ContributorDto>(data: new ContributorDto(), errorMessage: string.Empty);
            try
            {
                var reg = await _context.Contributor.FindAsync(id);
                if (reg != null)
                {
                    response.Data = new ContributorDto()
                    {
                        Id = reg.Id,
                        Biography = reg.Biography,
                        DateOfBirth = reg.DateOfBirth,
                        NickName = reg.NickName,
                        PhotoPath = reg.PhotoPath
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


        public async Task<int> PostAsync(ContributorDto contributorDto)
        {
            var newContributorDto = new Contributor
            {
                Id = contributorDto.Id,
                Biography = contributorDto.Biography,
                DateOfBirth = contributorDto.DateOfBirth,
                NickName = contributorDto.NickName,
                PhotoPath = contributorDto.PhotoPath,
                RegisteredAt = DateTime.UtcNow,
                AccountId = contributorDto.ProfileId
            };

            _context.Contributor.Add(newContributorDto);
            await _context.SaveChangesAsync();

            contributorDto.Id = newContributorDto.Id; // Set the ID of the new client

            return contributorDto.Id;
        }


        public async Task<ServiceResponse<ContributorDto>> PutAsync(int id, ContributorDto contributorDto)
        {
            var response = new ServiceResponse<ContributorDto>(data: new ContributorDto(), string.Empty);
            try
            {
                var result = await _context.Contributor.FindAsync(id);
                if (result != null)
                {
                    result.NickName = contributorDto?.NickName;
                    result.PhotoPath = contributorDto?.PhotoPath;
                    result.DateOfBirth = contributorDto?.DateOfBirth;
                    result.Biography = contributorDto?.Biography;

                    await _context.SaveChangesAsync();
                    response.Data = contributorDto;
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

        // Delete a client
        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<bool>(data: false, errorMessage: string.Empty);
            try
            {
                var client = await _context.Contributor.FindAsync(id);
                if (client != null)
                {
                    _context.Contributor.Remove(client);
                    await _context.SaveChangesAsync();
                    response.Data = true;
                }
                else
                    response.ErrorMessage = "Client not found.";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ha ocurrido un error mientras se eliminaba el registro: " + ex.Message;
            }
            return response;
        }
    }
}
