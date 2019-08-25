using System;
using System.Linq;

namespace PublicApi.v1.Mappers
{
    public class FoodItemWithCountsMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.FoodItemWithCounts))
            {
                return MapFromBLL((BLL.App.DTO.FoodItemWithCounts) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static PublicApi.v1.DTO.FoodItemWithCounts MapFromBLL(BLL.App.DTO.FoodItemWithCounts foodItem)
        {
            var res = foodItem == null ? null : new PublicApi.v1.DTO.FoodItemWithCounts()
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
                FoodItemTags = foodItem.FoodItemTags.Select(FoodItemTagMapper.MapFromBLL).ToList()
            };
            
            return res;
        }
    }
}