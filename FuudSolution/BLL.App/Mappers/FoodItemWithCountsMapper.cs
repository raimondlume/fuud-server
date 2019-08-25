using System;
using System.Linq;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FoodItemWithCountsMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodItemWithCounts))
            {
                return MapFromDAL((DAL.App.DTO.FoodItemWithCounts) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.FoodItemWithCounts MapFromDAL(DAL.App.DTO.FoodItemWithCounts foodItem)
        {
            var res = foodItem == null ? null : new BLL.App.DTO.FoodItemWithCounts()
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
                FoodItemTags = foodItem.FoodItemTags.Select(FoodItemTagMapper.MapFromDAL).ToList()
            };
            
            return res;
        }
    }
}