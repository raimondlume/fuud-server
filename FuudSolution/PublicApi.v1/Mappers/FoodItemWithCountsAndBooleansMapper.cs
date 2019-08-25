using System.Linq;
using PublicApi.v1.DTO;

namespace PublicApi.v1.Mappers
{
    public class FoodItemWithCountsAndBooleansMapper
    {
        public static PublicApi.v1.DTO.FoodItemWithCountsAndBooleans MapFromBLL(BLL.App.DTO.FoodItemWithCountsAndBooleans foodItem)
        {
            var res = foodItem == null ? null : new FoodItemWithCountsAndBooleans()
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromBLL(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromBLL(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst,
                CommentCount = foodItem.CommentCount,
                RatingCount = foodItem.RatingCount,
                DepletedReportCount = foodItem.DepletedReportCount,
                Prices = foodItem.Prices.Select(PriceMapper.MapFromBLL).ToList(),
                FoodItemTags = foodItem.FoodItemTags.Select(FoodItemTagMapper.MapFromBLL).ToList(),
                UserRating = foodItem.UserRating,
                HasUserMadeDepletedReport = foodItem.HasUserMadeDepletedReport
            };
            
            return res;
        }
    }
}