using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Services
{
    public class FoodItemService : BaseEntityService<BLL.App.DTO.FoodItem, DAL.App.DTO.FoodItem, IAppUnitOfWork>,
        IFoodItemService
    {
        public FoodItemService(IAppUnitOfWork uow) : base(uow, new FoodItemMapper())
        {
            ServiceRepository = Uow.FoodItems;
        }

        public async Task<List<FoodItemWithCounts>> AllActiveWithCountsAsync()
        {
            return (await Uow.FoodItems.AllActiveWithCountsAsync())
                .Select(FoodItemWithCountsMapper.MapFromDAL)
                .ToList();
        }

        public async Task<List<FoodItemWithCountsAndBooleans>> AllActiveWithCountsAndBooleansAsync(int userId)
        {
            return (await Uow.FoodItems.AllActiveWithCountsAndBooleansAsync(userId))
                .Select(FoodItemWithCountsAndBooleansMapper.MapFromDAL)
                .ToList();
        }

        public async Task<List<FoodItemWithCounts>> AllActiveWithCountsFromProviderAsync(int providerId)
        {
            return (await Uow.FoodItems.AllActiveWithCountsAsync())
                .Where(item => item.ProviderId == providerId)
                .Select(FoodItemWithCountsMapper.MapFromDAL)
                .ToList();
        }

        public async Task<List<FoodItemWithCountsAndBooleans>> AllActiveWithCountsAndBooleansFromProviderAsync(
            int providerId, int userId)
        {
            return (await Uow.FoodItems.AllActiveWithCountsAndBooleansAsync(userId))
                .Where(item => item.ProviderId == providerId)
                .Select(FoodItemWithCountsAndBooleansMapper.MapFromDAL)
                .ToList();
        }

        public void ArchiveAllActiveFoodItemsFromProvider(int providerId)
        {
            Uow.FoodItems.ArchiveAllActiveFoodItemsFromProvider(providerId);
        }
    }
}