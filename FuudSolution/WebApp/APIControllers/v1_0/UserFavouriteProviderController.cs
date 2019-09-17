using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using me.raimondlu.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicApi.v1.Mappers;

namespace WebApp.APIControllers.v1_0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserFavouriteProviderController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public UserFavouriteProviderController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/UserFavouriteProvider
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.UserFavouriteProvider>>> GetUserFavouriteProviders()
        {
            return (await _bll.UserFavouriteProviders.AllAsync())
                .Select(UserFavouriteProviderMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/UserFavouriteProvider/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.UserFavouriteProvider>> GetUserFavouriteProvider(int id)
        {
            var userFavouriteProvider = await _bll.UserFavouriteProviders.FindAsync(id);

            if (userFavouriteProvider == null)
            {
                return NotFound();
            }
            
            // check, that the object being used is really belongs to logged in user
            if (!await _bll.UserFavouriteProviders.BelongsToUserAsync(userFavouriteProvider.AppUserId, User.GetUserId()))
            {
                return NotFound();
            }

            return UserFavouriteProviderMapper.MapFromBLL(userFavouriteProvider);
        }

        /*// PUT: api/UserFavouriteProvider/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFavouriteProvider(int id,
            PublicApi.v1.DTO.UserFavouriteProvider userFavouriteProvider)
        {
            if (id != userFavouriteProvider.Id)
            {
                return BadRequest();
            }

            // check, that the object being used is really belongs to logged in user
            if (!await _bll.UserFavouriteProviders.BelongsToUserAsync(userFavouriteProvider.AppUserId, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.UserFavouriteProviders.Update(UserFavouriteProviderMapper.MapFromExternal(userFavouriteProvider));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/UserFavouriteProvider
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.UserFavouriteProvider>> PostUserFavouriteProvider(
            PublicApi.v1.DTO.UserFavouriteProvider userFavouriteProvider)
        {
            // check that the person sending the report is the logged in user
            if (userFavouriteProvider.AppUserId != User.GetUserId())
            {
                return StatusCode(403);
            }
            
            // get the enitity back with attached state id - (- maxint)
            userFavouriteProvider = PublicApi.v1.Mappers.UserFavouriteProviderMapper.MapFromBLL(
                _bll.UserFavouriteProviders.Add(PublicApi.v1.Mappers.UserFavouriteProviderMapper.MapFromExternal(userFavouriteProvider)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            userFavouriteProvider = PublicApi.v1.Mappers.UserFavouriteProviderMapper.MapFromBLL(
                _bll.UserFavouriteProviders.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.UserFavouriteProviderMapper.MapFromExternal(userFavouriteProvider)));


            return CreatedAtAction("GetUserFavouriteProvider", new {id = userFavouriteProvider.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, userFavouriteProvider);
        }

        // DELETE: api/UserFavouriteProvider/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.UserFavouriteProvider>> DeleteUserFavouriteProvider(int id)
        {
            var userFavouriteProvider = await _bll.UserFavouriteProviders.FindAsync(id);
            if (userFavouriteProvider == null)
            {
                return NotFound();
            }
            
            // check, that the object being used is really belongs to logged in user
            if (!await _bll.UserFavouriteProviders.BelongsToUserAsync(userFavouriteProvider.AppUserId, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.UserFavouriteProviders.Remove(userFavouriteProvider);
            await _bll.SaveChangesAsync();

            return UserFavouriteProviderMapper.MapFromBLL(userFavouriteProvider);
        }*/
    }
}