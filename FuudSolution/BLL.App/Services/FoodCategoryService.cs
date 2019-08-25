using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;

namespace BLL.App.Services
{
    public class FoodCategoryService :
        BaseEntityService<BLL.App.DTO.FoodCategory, DAL.App.DTO.FoodCategory, IAppUnitOfWork>, IFoodCategoryService
    {
        public FoodCategoryService(IAppUnitOfWork uow) : base(uow, new FoodCategoryMapper())
        {
            ServiceRepository = Uow.FoodCategories;
        }
    }
}