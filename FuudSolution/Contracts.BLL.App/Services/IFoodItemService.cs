using System.Collections.Generic;
using System.Threading.Tasks;
using me.raimondlu.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;


namespace Contracts.BLL.App.Services
{
    public interface IFoodItemService : IBaseEntityService<BLLAppDTO.FoodItem>, IFoodItemRepository<BLLAppDTO.FoodItem>
    {
        Task<List<BLLAppDTO.FoodItemWithCounts>> AllActiveWithCountsAsync();
        
        Task<List<BLLAppDTO.FoodItemWithCountsAndBooleans>> AllActiveWithCountsAndBooleansAsync(int userId);

        Task<List<BLLAppDTO.FoodItemWithCounts>> AllActiveWithCountsFromProviderAsync(int providerId);

        Task<List<BLLAppDTO.FoodItemWithCountsAndBooleans>> AllActiveWithCountsAndBooleansFromProviderAsync(
            int providerId, int userId);

        void ArchiveAllActiveFoodItemsFromProvider(int providerId);
    }
}