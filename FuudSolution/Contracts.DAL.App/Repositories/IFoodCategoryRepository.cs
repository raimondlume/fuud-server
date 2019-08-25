using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodCategoryRepository : IFoodCategoryRepository<DALAppDTO.FoodCategory>
    {
        
    }
    
    public interface IFoodCategoryRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {      
        
    }
}