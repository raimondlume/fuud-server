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
    public class FoodCategoryController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public FoodCategoryController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/FoodCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.FoodCategory>>> GetFoodCategories()
        {
            return (await _bll.FoodCategories.AllAsync())
                .Select(FoodCategoryMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/FoodCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodCategory>> GetFoodCategory(int id)
        {
            var foodCategory = await _bll.FoodCategories.FindAsync(id);

            if (foodCategory == null)
            {
                return NotFound();
            }

            return FoodCategoryMapper.MapFromBLL(foodCategory);
        }

        /*// PUT: api/FoodCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodCategory(int id, PublicApi.v1.DTO.FoodCategory foodCategory)
        {
            if (id != foodCategory.Id)
            {
                return BadRequest();
            }

            _bll.FoodCategories.Update(FoodCategoryMapper.MapFromExternal(foodCategory));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/FoodCategory
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodCategory>> PostFoodCategory(
            PublicApi.v1.DTO.FoodCategory foodCategory)
        {
            // get the enitity back with attached state id - (- maxint)
            foodCategory = PublicApi.v1.Mappers.FoodCategoryMapper.MapFromBLL(
                _bll.FoodCategories.Add(PublicApi.v1.Mappers.FoodCategoryMapper.MapFromExternal(foodCategory)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            foodCategory = PublicApi.v1.Mappers.FoodCategoryMapper.MapFromBLL(
                _bll.FoodCategories.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.FoodCategoryMapper.MapFromExternal(foodCategory)));


            return CreatedAtAction("GetFoodCategory", new {id = foodCategory.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, foodCategory);
        }

        // DELETE: api/FoodCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodCategory>> DeleteFoodCategory(int id)
        {
            var foodCategory = await _bll.FoodCategories.FindAsync(id);
            if (foodCategory == null)
            {
                return NotFound();
            }

            _bll.FoodCategories.Remove(foodCategory);
            await _bll.SaveChangesAsync();

            return FoodCategoryMapper.MapFromBLL(foodCategory);
        }*/
    }
}