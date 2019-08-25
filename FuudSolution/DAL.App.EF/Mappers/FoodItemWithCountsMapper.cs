using System;
using System.Linq;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class FoodItemWithCountsMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodItemWithCounts))
            {
                return MapFromDomain((Domain.FoodItem) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.FoodItemWithCounts MapFromDomain(Domain.FoodItem foodItem)
        {
            var res = foodItem == null ? null : new DAL.App.DTO.FoodItemWithCounts()
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromDomain(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromDomain(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst,
                CommentCount = foodItem.Comments.Count,
                RatingCount = foodItem.Ratings.Sum(rating => rating.RatingValue),
                DepletedReportCount = foodItem.DepletedReports.Count,
                Prices = foodItem.Prices.Select(PriceMapper.MapFromDomain).ToList(),
                FoodItemTags = foodItem.FoodItemTags.Select(FoodItemTagMapper.MapFromDomain).ToList()
            };
            
            return res;
        }
    }
}