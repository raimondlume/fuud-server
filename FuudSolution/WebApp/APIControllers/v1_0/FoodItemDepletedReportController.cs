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
    public class FoodItemDepletedReportController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public FoodItemDepletedReportController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/FoodItemDepletedReport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.FoodItemDepletedReport>>>
            GetFoodItemDepletedReports()
        {
            return (await _bll.FoodItemDepletedReports.AllAsync())
                .Select(FoodItemDepletedReportMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/FoodItemDepletedReport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItemDepletedReport>> GetFoodItemDepletedReport(int id)
        {
            var foodItemDepletedReport = await _bll.FoodItemDepletedReports.FindAsync(id);

            if (foodItemDepletedReport == null)
            {
                return NotFound();
            }

            return FoodItemDepletedReportMapper.MapFromBLL(foodItemDepletedReport);
        }

        // PUT: api/FoodItemDepletedReport/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutFoodItemDepletedReport(int id,
            PublicApi.v1.DTO.FoodItemDepletedReport foodItemDepletedReport)
        {
            if (id != foodItemDepletedReport.Id)
            {
                return BadRequest();
            }
            
            // check, that the object being used is really belongs to logged in user
            if (!await _bll.FoodItemDepletedReports.BelongsToUserAsync(foodItemDepletedReport.AppUserId, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.FoodItemDepletedReports.Update(FoodItemDepletedReportMapper.MapFromExternal(foodItemDepletedReport));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/FoodItemDepletedReport
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItemDepletedReport>> PostFoodItemDepletedReport(
            PublicApi.v1.DTO.FoodItemDepletedReport foodItemDepletedReport)
        {
            // check that the person sending the report is the logged in user
            if (foodItemDepletedReport.AppUserId != User.GetUserId())
            {
                return StatusCode(403);
            }
            
            // get the enitity back with attached state id - (- maxint)
            foodItemDepletedReport = PublicApi.v1.Mappers.FoodItemDepletedReportMapper.MapFromBLL(
                _bll.FoodItemDepletedReports.Add(PublicApi.v1.Mappers.FoodItemDepletedReportMapper.MapFromExternal(foodItemDepletedReport)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            foodItemDepletedReport = PublicApi.v1.Mappers.FoodItemDepletedReportMapper.MapFromBLL(
                _bll.FoodItemDepletedReports.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.FoodItemDepletedReportMapper.MapFromExternal(foodItemDepletedReport)));


            return CreatedAtAction("GetFoodItemDepletedReport", new {id = foodItemDepletedReport.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, foodItemDepletedReport);
        }

        // DELETE: api/FoodItemDepletedReport/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PublicApi.v1.DTO.FoodItemDepletedReport>> DeleteFoodItemDepletedReport(int id)
        {
            var foodItemDepletedReport = await _bll.FoodItemDepletedReports.FindAsync(id);
            if (foodItemDepletedReport == null)
            {
                return NotFound();
            }
            
            // check, that the object being used is really belongs to logged in user
            if (!await _bll.FoodItemDepletedReports.BelongsToUserAsync(foodItemDepletedReport.AppUserId, User.GetUserId()))
            {
                return NotFound();
            }

            _bll.FoodItemDepletedReports.Remove(foodItemDepletedReport);
            await _bll.SaveChangesAsync();

            return FoodItemDepletedReportMapper.MapFromBLL(foodItemDepletedReport);
        }
    }
}