using me.raimondlu.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IPriceService : IBaseEntityService<Price>, IPriceRepository<Price>
    {
        
    }
}