using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.WebApp.Core.Entities;
using MediaLibrary.WebApp.Server.Services;
using MediaLibrary.WebApp.Shared.DTOs;
using MediaLibrary.WebApp.Server.Services.Contracts;
using MediaLibrary.WebApp.Shared.Overviews;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MediaLibrary.WebApp.Server.Helpers.Contracts;
using MediaLibrary.WebApp.Server.Helpers;
using MediaLibrary.WebApp.Core;

namespace MediaLibrary.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorDetailController : ControllerBase
    {
        private readonly IContributorDetailService _contributorDetailService; 
        private readonly IUserHelper _userHelper; 

        public ContributorDetailController(IContributorDetailService contributorDetailService, IUserHelper userHelper)
        {
            _contributorDetailService  = contributorDetailService;
            _userHelper = userHelper;
        }

        // GET: api/ContributorDetail
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<ContributorDto>>?> GetAllAsync()
        {
            try
            {
                var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
                if (user == null)
                {
                    return NotFound();
                }

                var items = await _contributorDetailService.GetDtoAllAsync(user.Id, user.UserType);
                return Ok(items);
            }
            catch (InvalidByteRangeException ex)
            {
                BadRequest(ex.Message);
            }
            return null;
        }
    }
}