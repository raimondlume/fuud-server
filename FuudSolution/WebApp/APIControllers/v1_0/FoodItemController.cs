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
    public class FoodItemController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public FoodItemController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/FoodItem
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.FoodItemWithCounts>>> GetFoodItems()
        {
            return (await _bll.FoodItems.AllActiveWithCountsAsync())
                .Select(FoodItemWithCountsMapper.MapFromBLL)
                .ToList();
        }
        
        // GET: api/FoodItem
        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.FoodItemWithCountsAndBooleans>>> GetFoodItemsWithRatings()
        {
            return (await _bll.FoodItems.AllActiveWithCountsAndBooleansAsync(User.GetUserId()))
                .Select(FoodItemWithCountsAndBooleansMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/FoodItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItem>> GetFoodItem(int id)
        {
            var foodItem = await _bll.FoodItems.FindAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return FoodItemMapper.MapFromBLL(foodItem);
        }
        
        // GET: api/FoodItem/provider/5
        [HttpGet("provider/{providerId}")]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.FoodItemWithCounts>>> GetFoodItemsFromProvider(int providerId)
        {
            return (await _bll.FoodItems.AllActiveWithCountsFromProviderAsync(providerId))
                .Select(FoodItemWithCountsMapper.MapFromBLL)
                .ToList();
        }
        
        // GET: api/FoodItem/provider/5/user
        [HttpGet("provider/{providerId}/user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.FoodItemWithCountsAndBooleans>>> GetFoodItemsFromProviderWithRatings(int providerId)
        {
            return (await _bll.FoodItems.AllActiveWithCountsAndBooleansFromProviderAsync(providerId, User.GetUserId()))
                .Select(FoodItemWithCountsAndBooleansMapper.MapFromBLL)
                .ToList();
        }

        /*// PUT: api/FoodItem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(int id, PublicApi.v1.DTO.FoodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return BadRequest();
            }

            _bll.FoodItems.Update(FoodItemMapper.MapFromExternal(foodItem));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/FoodItem
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItem>> PostFoodItem(PublicApi.v1.DTO.FoodItem foodItem)
        {
            // get the enitity back with attached state id - (- maxint)
            foodItem = PublicApi.v1.Mappers.FoodItemMapper.MapFromBLL(
                _bll.FoodItems.Add(PublicApi.v1.Mappers.FoodItemMapper.MapFromExternal(foodItem)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            foodItem = PublicApi.v1.Mappers.FoodItemMapper.MapFromBLL(
                _bll.FoodItems.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.FoodItemMapper.MapFromExternal(foodItem)));


            return CreatedAtAction("GetFoodItem", new {id = foodItem.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, foodItem);
        }

        // DELETE: api/FoodItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItem>> DeleteFoodItem(int id)
        {
            var foodItem = await _bll.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _bll.FoodItems.Remove(foodItem);
            await _bll.SaveChangesAsync();

            return FoodItemMapper.MapFromBLL(foodItem);
        }*/
    }
}