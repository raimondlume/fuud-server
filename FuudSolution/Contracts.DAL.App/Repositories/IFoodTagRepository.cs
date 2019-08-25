using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodTagRepository : IFoodTagRepository<DALAppDTO.FoodTag>
    {
        
    }
    
    public interface IFoodTagRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {      
        
    }
}