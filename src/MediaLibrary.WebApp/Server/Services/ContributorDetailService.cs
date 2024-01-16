using MediaLibrary.WebApp.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.WebApp.Server.Services;
using MediaLibrary.WebApp.Server.Services.Contracts;
using MediaLibrary.WebApp.Server.Data;

namespace MediaLibrary.WebApp.Server.Services
{
    public class ContributorDetailService : IContributorDetailService
    {
        DataContext _context;  
        public ContributorDetailService(DataContext context)
        {
                _context = context;
        }

        public async Task<List<ContributorDto>> GetDtoAllAsync(string userId)
        {
            var regs = await (from reg in _context.Contributor.Where(c => c.AccountId == userId) select reg).ToListAsync();
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
    }
}
