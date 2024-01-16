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
    public class CountryService : ICountryService
    {
        private readonly DataContext _context;

        public CountryService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CountryDto>> GetDtoAllAsync()
        {
            var regs = await (from reg in _context.Country select reg).ToListAsync();
            List<CountryDto> result = new List<CountryDto>();
            foreach (var reg in regs)
            {
                CountryDto regNew = new CountryDto
                {
                    Id = reg.Id,
                    Name = reg.Name
                };
                result.Add(regNew);
            }
            return result;
        }

        public async Task<ServiceResponse<CountryDto>> GetDtoAsync(int id)
        {
            var response = new ServiceResponse<CountryDto>(data: new CountryDto(), errorMessage: string.Empty);
            try
            {
                var reg = await _context.Country.FindAsync(id);
                if (reg != null)
                {
                    response.Data = new CountryDto()
                    {
                        Id = reg.Id,
                        Name = reg.Name
                    };
                }
                else
                    response.ErrorMessage = "Registro no encontrado";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ha ocurrido un error mientras se recuperaba la información: " + ex.Message;
            }
            return response;
        }


        public async Task<ServiceResponse<CountryDto>> Post(CountryDto CountryDto)
        {
            var response = new ServiceResponse<CountryDto>(data: new CountryDto(), string.Empty);
            try
            {
                var newCountryDto = new Country
                {
                    Id = CountryDto.Id,
                    Name = CountryDto.Name
                };

                _context.Country.Add(newCountryDto);
                await _context.SaveChangesAsync();

                CountryDto.Id = newCountryDto.Id; // Set the ID of the new client
                response.Data = CountryDto;
            }
            catch (DbUpdateException ex)
            {
                // Handle the specific exception. You can also log the error here.
                response.ErrorMessage = "Error mientras se creaba el registro: " + ex.InnerException?.Message;
            }
            catch (Exception ex)
            {
                // Handle other types of exceptions if needed
                response.ErrorMessage = "Error mientras se creaba el registro: " + ex.InnerException?.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<CountryDto>> Put(int id, CountryDto CountryDto)
        {
            var response = new ServiceResponse<CountryDto>(data: new CountryDto(), string.Empty);
            try
            {
                var result = await _context.Country.FindAsync(id);
                if (result != null)
                {
                    result.Name = CountryDto?.Name;

                    await _context.SaveChangesAsync();
                    response.Data = CountryDto;
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
        public async Task<ServiceResponse<bool>> Delete(int id)
        {
            var response = new ServiceResponse<bool>(data: false, errorMessage: string.Empty);
            try
            {
                var result = await _context.Country.FindAsync(id);
                if (result != null)
                {
                    _context.Country.Remove(result);
                    await _context.SaveChangesAsync();
                    response.Data = true;
                }
                else
                    response.ErrorMessage = "Registro no encontrado";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ha ocurrido un error mientras se eliminaba el registro: " + ex.Message;
            }
            return response;
        }
    }
}
