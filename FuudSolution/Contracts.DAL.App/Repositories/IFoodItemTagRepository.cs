using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodItemTagRepository : IFoodItemTagRepository<DALAppDTO.FoodItemTag>
    {
        
    }
    
    public interface IFoodItemTagRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {      
        
    }
}