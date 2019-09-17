using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.WebCrawler.Base;
using Microsoft.AspNetCore.Mvc;
using PublicApi.v1.DTO;
using PublicApi.v1.Mappers;

namespace WebApp.APIControllers.v1_0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FoodTagController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public FoodTagController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/FoodTag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodTag>>> GetFoodTags()
        {
            return (await _bll.FoodTags.AllAsync())
                .Select(FoodTagMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/FoodTag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodTag>> GetFoodTag(int id)
        {
            var foodTag = await _bll.FoodTags.FindAsync(id);

            if (foodTag == null)
            {
                return NotFound();
            }

            return FoodTagMapper.MapFromBLL(foodTag);
        }

        /*// PUT: api/FoodTag/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodTag(int id, FoodTag foodTag)
        {
            if (id != foodTag.Id)
            {
                return BadRequest();
            }

            _bll.FoodTags.Update(FoodTagMapper.MapFromExternal(foodTag));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/FoodTag
        [HttpPost]
        public async Task<ActionResult<FoodTag>> PostFoodTag(FoodTag foodTag)
        {
            // get the enitity back with attached state id - (- maxint)
            foodTag = FoodTagMapper.MapFromBLL(
                _bll.FoodTags.Add(FoodTagMapper.MapFromExternal(foodTag)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            foodTag = FoodTagMapper.MapFromBLL(
                _bll.FoodTags.GetUpdatesAfterUOWSaveChanges(
                    FoodTagMapper.MapFromExternal(foodTag)));


            return CreatedAtAction("GetFoodTag", new {id = foodTag.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, foodTag);
        }

        // DELETE: api/FoodTag/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodTag>> DeleteFoodTag(int id)
        {
            var foodTag = await _bll.FoodTags.FindAsync(id);
            if (foodTag == null)
            {
                return NotFound();
            }

            _bll.FoodTags.Remove(foodTag);
            await _bll.SaveChangesAsync();

            return FoodTagMapper.MapFromBLL(foodTag);
        }*/
    }
}