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
    public class ProviderController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProviderController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Provider
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicApi.v1.DTO.Provider>>> GetProviders()
        {
            return (await _bll.Providers.AllAsync())
                .Select(ProviderMapper.MapFromBLL)
                .ToList();
        }

        // GET: api/Provider/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Provider>> GetProvider(int id)
        {
            var provider = await _bll.Providers.FindAsync(id);

            if (provider == null)
            {
                return NotFound();
            }

            return ProviderMapper.MapFromBLL(provider);
        }

        /*// PUT: api/Provider/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvider(int id, PublicApi.v1.DTO.Provider provider)
        {
            if (id != provider.Id)
            {
                return BadRequest();
            }

            _bll.Providers.Update(ProviderMapper.MapFromExternal(provider));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Provider
        [HttpPost]
        public async Task<ActionResult<PublicApi.v1.DTO.Provider>> PostProvider(PublicApi.v1.DTO.Provider provider)
        {
            // get the enitity back with attached state id - (- maxint)
            provider = PublicApi.v1.Mappers.ProviderMapper.MapFromBLL(
                _bll.Providers.Add(PublicApi.v1.Mappers.ProviderMapper.MapFromExternal(provider)));
            // ef will update its internally tracked entities
            await _bll.SaveChangesAsync();
            // get the updated entity, now with ID from database
            provider = PublicApi.v1.Mappers.ProviderMapper.MapFromBLL(
                _bll.Providers.GetUpdatesAfterUOWSaveChanges(
                    PublicApi.v1.Mappers.ProviderMapper.MapFromExternal(provider)));


            return CreatedAtAction("GetProvider",
                new {id = provider.Id, version = HttpContext.GetRequestedApiVersion().ToString()}, provider);
        }

        // DELETE: api/Provider/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.v1.DTO.Provider>> DeleteProvider(int id)
        {
            var provider = await _bll.Providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            _bll.Providers.Remove(provider);
            await _bll.SaveChangesAsync();

            return ProviderMapper.MapFromBLL(provider);
        }*/
    }
}