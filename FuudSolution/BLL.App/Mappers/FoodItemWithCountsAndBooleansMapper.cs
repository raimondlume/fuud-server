using System;
using System.Linq;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FoodItemWithCountsAndBooleansMapper
    {
        public static BLL.App.DTO.FoodItemWithCountsAndBooleans MapFromDAL(DAL.App.DTO.FoodItemWithCountsAndBooleans foodItem, int userId)
        {
            var res = foodItem == null ? null : new BLL.App.DTO.FoodItemWithCountsAndBooleans()
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromDAL(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromDAL(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst,
                CommentCount = foodItem.CommentCount,
                RatingCount = foodItem.RatingCount,
                DepletedReportCount = foodItem.DepletedReportCount,
                Prices = foodItem.Prices.Select(PriceMapper.MapFromDAL).ToList(),
                FoodItemTags = foodItem.FoodItemTags.Select(FoodItemTagMapper.MapFromDAL).ToList(),
                UserRating = foodItem.UserRating,
                HasUserMadeDepletedReport = foodItem.HasUserMadeDepletedReport
            };
            
            return res;
        }
    }
}