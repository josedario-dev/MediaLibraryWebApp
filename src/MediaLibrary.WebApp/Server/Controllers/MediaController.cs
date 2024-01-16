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
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;
        private readonly string _container;

        public MediaController(IMediaService mediaService, IUserHelper userHelper, IFileStorage fileStorage)
        {
            _mediaService = mediaService;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
            _container = "medias";
        }

        // GET: api/Media/5
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<MediaDto?>>> GetAllAsync()
        {
            try
            {
                var items = await _mediaService.GetDtoAllAsync();
                return Ok(items);
            }
            catch (InvalidByteRangeException ex)
            {
                BadRequest(ex.Message);
            }
            return null;
        }

        // GET: api/Media/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var item = await _mediaService.GetDtoAsync(id);
                return Ok(item.Data);
            }
            catch (InvalidByteRangeException ex)
            {
                BadRequest(ex.Message);
            }
            return null;
        }

        // POST: api/Media
        [HttpPost]
        public async Task<ActionResult<MediaDto>> Post(MediaDto mediaDto)
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

                if (!string.IsNullOrEmpty(mediaDto.File))
                {
                    var file = Convert.FromBase64String(mediaDto.File);
                    mediaDto.FilePath = await _fileStorage.SaveFileAsync(file, $"{mediaDto.Extension}", _container);
                }

                var id = await _mediaService.PostAsync(mediaDto);

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

        // PUT: api/Media/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MediaDto MediaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedReg = await _mediaService.PutAsync(id, MediaDto);
            if (updatedReg == null)
                return NotFound();

            return Ok(updatedReg);
        }

        // DELETE: api/Media/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _mediaService.DeleteAsync(id);
            if (!result.Data)
                return NotFound();

            return NoContent();
        }
    }
}
