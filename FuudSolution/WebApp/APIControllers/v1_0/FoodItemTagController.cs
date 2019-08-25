using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using PublicApi.v1.Mappers;

namespace WebApp.APIControllers.v1_0
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FoodItemTagController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public FoodItemTagController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/FoodItemTag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.FoodItemTag>>> GetFoodItemTags()
        {
            return (await _bll.FoodItemTags.AllAsync())
                .Select(FoodItemTagMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/FoodItemTag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItemTag>> GetFoodItemTag(int id)
        {
            var foodItemTag = await _bll.FoodItemTags.FindAsync(id);

            if (foodItemTag == null)
            {
                return NotFound();
            }

            return FoodItemTagMapper.MapFromBLL(foodItemTag);
        }

        // PUT: api/FoodItemTag/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItemTag(int id, PublicApi.v1.DTO.FoodItemTag foodItemTag)
        {
            if (id != foodItemTag.Id)
            {
                return BadRequest();
            }

            _bll.FoodItemTags.Update(FoodItemTagMapper.MapFromExternal(foodItemTag));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/FoodItemTag
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItemTag>> PostFoodItemTag(
            PublicApi.v1.DTO.FoodItemTag foodItemTag)
        {
            // get the enitity back with attached state id - (- maxint)
            foodItemTag = PublicApi.v1.Mappers.FoodItemTagMapper.MapFromBLL(
                _bll.FoodItemTags.Add(PublicApi.v1.Mappers.FoodItemTagMapper.MapFromExternal(foodItemTag)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            foodItemTag = PublicApi.v1.Mappers.FoodItemTagMapper.MapFromBLL(
                _bll.FoodItemTags.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.FoodItemTagMapper.MapFromExternal(foodItemTag)));


            return CreatedAtAction("GetFoodItemTag",
                new {id = foodItemTag.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, foodItemTag);
        }

        // DELETE: api/FoodItemTag/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItemTag>> DeleteFoodItemTag(int id)
        {
            var foodItemTag = await _bll.FoodItemTags.FindAsync(id);
            if (foodItemTag == null)
            {
                return NotFound();
            }

            _bll.FoodItemTags.Remove(foodItemTag);
            await _bll.SaveChangesAsync();

            return FoodItemTagMapper.MapFromBLL(foodItemTag);
        }
    }
}