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
    public class PriceController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public PriceController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Price
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Price>>> GetPrices()
        {
            return (await _bll.Prices.AllAsync())
                .Select(PriceMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/Price/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Price>> GetPrice(int id)
        {
            var price = await _bll.Prices.FindAsync(id);

            if (price == null)
            {
                return NotFound();
            }

            return PriceMapper.MapFromBLL(price);
        }

        // PUT: api/Price/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrice(int id, PublicApi.v1.DTO.Price price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            _bll.Prices.Update(PriceMapper.MapFromExternal(price));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Price
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.Price>> PostPrice(PublicApi.v1.DTO.Price price)
        {
            // get the enitity back with attached state id - (- maxint)
            price = PublicApi.v1.Mappers.PriceMapper.MapFromBLL(
                _bll.Prices.Add(PublicApi.v1.Mappers.PriceMapper.MapFromExternal(price)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            price = PublicApi.v1.Mappers.PriceMapper.MapFromBLL(
                _bll.Prices.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.PriceMapper.MapFromExternal(price)));


            return CreatedAtAction("GetPrice",
                new {id = price.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, price);
        }

        // DELETE: api/Price/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Price>> DeletePrice(int id)
        {
            var price = await _bll.Prices.FindAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            _bll.Prices.Remove(price);
            await _bll.SaveChangesAsync();

            return PriceMapper.MapFromBLL(price);
        }
    }
}