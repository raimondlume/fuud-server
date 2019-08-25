using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Services
{
    public class FoodItemTagService : BaseEntityService<BLL.App.DTO.FoodItemTag, DAL.App.DTO.FoodItemTag, IAppUnitOfWork>, IFoodItemTagService
    {
        public FoodItemTagService(IAppUnitOfWork uow) : base(uow, new FoodItemTagMapper())
        {
            ServiceRepository = Uow.FoodItemTags;
        }
    }
}