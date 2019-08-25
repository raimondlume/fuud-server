using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Services
{
    public class ProviderService : BaseEntityService<BLL.App.DTO.Provider, DAL.App.DTO.Provider, IAppUnitOfWork>, IProviderService
    {
        public ProviderService(IAppUnitOfWork uow) : base(uow, new ProviderMapper())
        {
            ServiceRepository = Uow.Providers;
        }
    }
}