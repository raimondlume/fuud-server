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
    public class CommentController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public CommentController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get all comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Comment>>> GetComments()
        {
            return (await _bll.Comments.AllAsync())
                .Select(CommentMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Comment>>> GetComment(int id)
        {
            return (await _bll.Comments.AllForFoodItemAsync(id))
                .Select(CommentMapper.MapFromBLL)
                .ToList();
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutComment(int id, PublicApi.v1.DTO.Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _bll.Comments.Update(CommentMapper.MapFromExternal(comment));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Comment
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PublicApi.v1.DTO.Comment>> PostComment(PublicApi.v1.DTO.Comment comment)
        {
            // check that the person sending the request is not commenting on somebodies behalf
            if (comment.AppUserId != User.GetUserId())
            {
                return StatusCode(403);
            }
            
            // get the enitity back with attached state id - (- maxint)
            comment = PublicApi.v1.Mappers.CommentMapper.MapFromBLL(
                _bll.Comments.Add(PublicApi.v1.Mappers.CommentMapper.MapFromExternal(comment)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            comment = PublicApi.v1.Mappers.CommentMapper.MapFromBLL(
                _bll.Comments.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.CommentMapper.MapFromExternal(comment)));


            return CreatedAtAction("GetComment", new {id = comment.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, comment);
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PublicApi.v1.DTO.Comment>> DeleteComment(int id)
        {
            var comment = await _bll.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            
            // check, that the object being used is really belongs to logged in user
            if (!await _bll.Comments.BelongsToUserAsync(comment.AppUserId, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.Comments.Remove(comment);
            await _bll.SaveChangesAsync();

            return CommentMapper.MapFromBLL(comment);
        }
    }
}