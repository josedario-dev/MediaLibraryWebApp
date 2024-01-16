using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediaLibrary.WebApp.Core;
using MediaLibrary.WebApp.Server.Services;
using MediaLibrary.WebApp.Shared.DTOs;
using MediaLibrary.WebApp.Server.Services.Contracts;
using MediaLibrary.WebApp.Shared.Overviews;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MediaLibrary.WebApp.Core.Entities;
using MediaLibrary.WebApp.Server.Helpers.Contracts;
using MediaLibrary.WebApp.Server.Helpers;

namespace MediaLibrary.WebApp.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorsController : ControllerBase
    {
        private readonly IContributorService _contributorService;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;
        private readonly string _container;

        public ContributorsController(IContributorService contributorService, IUserHelper userHelper, IFileStorage fileStorage)
        {
            _contributorService = contributorService;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
            _container = "users";
        }

        // GET: api/Contributors/5
        [HttpGet]
        public async Task<ActionResult<List<ContributorDto?>>> GetAllAsync()
        {
            try
            {
                var items = await _contributorService.GetDtoAllAsync();
                return Ok(items);
            }
            catch (InvalidByteRangeException ex)
            {
                BadRequest(ex.Message);
            }
            return null;
        }

        // GET: api/Contributors/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var item = await _contributorService.GetDtoAsync(id);
                return Ok(item.Data);
            }
            catch (InvalidByteRangeException ex)
            {
                BadRequest(ex.Message);
            }
            return null;
        }

        // POST: api/Contributors
        [HttpPost]
        public async Task<ActionResult<ContributorDto>> Post(ContributorDto contributorDto)
        {
            try
            {
                var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
                if (user == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!string.IsNullOrEmpty(contributorDto.Photo))
                {
                    var photoUser = Convert.FromBase64String(contributorDto.Photo);
                    contributorDto.PhotoPath = await _fileStorage.SaveFileAsync(photoUser, ".jpg", _container);
                }

                contributorDto.ProfileId = user.Id;
                var id = await _contributorService.PostAsync(contributorDto);

                if (id != 0) 
                {
                    return Ok(id);
                }
                else
                {
                    return BadRequest("No se pudo crear");
                }

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe una ciudad con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        // PUT: api/Contributors/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContributorDto contributorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedReg = await _contributorService.PutAsync(id, contributorDto);
            if (updatedReg == null)
                return NotFound();

            return Ok(updatedReg);
        }

        // DELETE: api/Contributors/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _contributorService.DeleteAsync(id);
            if (!result.Data)
                return NotFound();

            return NoContent();
        }
    }
}
