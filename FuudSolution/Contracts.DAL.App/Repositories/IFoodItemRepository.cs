using System.Collections.Generic;
using System.Threading.Tasks;
using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodItemRepository : IFoodItemRepository<DALAppDTO.FoodItem>
    {
        Task<List<DALAppDTO.FoodItemWithCounts>> AllActiveWithCountsAsync();
        
        Task<List<DALAppDTO.FoodItemWithCountsAndBooleans>> AllActiveWithCountsAndBooleansAsync(int userId);
        

        void ArchiveAllActiveFoodItemsFromProvider(int providerId);
    }
    
    public interface IFoodItemRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {      
        
    }
}