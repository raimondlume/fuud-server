using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;

namespace BLL.App.Services
{
    public class FoodTagService : BaseEntityService<BLL.App.DTO.FoodTag, DAL.App.DTO.FoodTag, IAppUnitOfWork>, IFoodTagService
    {
        public FoodTagService(IAppUnitOfWork uow) : base(uow, new FoodTagMapper())
        {
            ServiceRepository = Uow.FoodTags;
        }
    }
}