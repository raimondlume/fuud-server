using BLL.App.DTO;
using me.raimondlu.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IFoodCategoryService : IBaseEntityService<FoodCategory>, IFoodCategoryRepository<FoodCategory>
    {
        
    }
}