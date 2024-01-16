using MediaLibrary.WebApp.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.WebApp.Server.Services;
using MediaLibrary.WebApp.Server.Services.Contracts;
using MediaLibrary.WebApp.Server.Data;
using MediaLibrary.WebApp.Client.Pages;
using Org.BouncyCastle.Tls;
using MediaLibrary.WebApp.Core;
using MediaLibrary.WebApp.Core.Enums;

namespace MediaLibrary.WebApp.Server.Services
{
    public class ContributorDetailService : IContributorDetailService
    {
        DataContext _context;
        public ContributorDetailService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ContributorDto>> GetDtoAllAsync(string? userId, UserType userType)
        {
            List<Core.Entities.Contributor> regs = null;
            if(userType == UserType.Admin)
                regs = await _context.Contributor.ToListAsync();
            else
                regs = await _context.Contributor.Where(t=>t.AccountId == userId).ToListAsync();

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
