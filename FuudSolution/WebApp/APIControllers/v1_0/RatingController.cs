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
    public class RatingController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public RatingController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Rating>>> GetRatings()
        {
            return (await _bll.Ratings.AllAsync())
                .Select(RatingMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Rating>> GetRating(int id)
        {
            var rating = await _bll.Ratings.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return RatingMapper.MapFromBLL(rating);
        }

        // PUT: api/Rating/5
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, PublicApi.v1.DTO.Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }

            // check, that the object being used is really belongs to logged in user
            if (!await _bll.Ratings.BelongsToUserAsync(rating.AppUserId, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.Ratings.Update(RatingMapper.MapFromExternal(rating));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Rating
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PublicApi.v1.DTO.Rating>> PostRating(PublicApi.v1.DTO.Rating rating)
        {
            // check that the person sending the rating is the logged in user
            if (rating.AppUserId != User.GetUserId())
            {
                return StatusCode(403);
            }

            // get the enitity back with attached state id - (- maxint)
            rating = PublicApi.v1.Mappers.RatingMapper.MapFromBLL(
                _bll.Ratings.Add(PublicApi.v1.Mappers.RatingMapper.MapFromExternal(rating)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            rating = PublicApi.v1.Mappers.RatingMapper.MapFromBLL(
                _bll.Ratings.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.RatingMapper.MapFromExternal(rating)));


            return CreatedAtAction("GetRating",
                new {id = rating.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, rating);
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PublicApi.v1.DTO.Rating>> DeleteRating(int id)
        {
            var rating = await _bll.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            // check, that the object being used is really belongs to logged in user
            if (!await _bll.Ratings.BelongsToUserAsync(rating.AppUserId, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.Ratings.Remove(rating);
            await _bll.SaveChangesAsync();

            return RatingMapper.MapFromBLL(rating);
        }
    }
}