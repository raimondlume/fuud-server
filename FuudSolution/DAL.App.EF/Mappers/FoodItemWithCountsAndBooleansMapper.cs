using System;
using System.Linq;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;
using FoodItem = Domain.FoodItem;

namespace DAL.App.EF.Mappers
{
    public class FoodItemWithCountsAndBooleansMapper
    {
        public static DAL.App.DTO.FoodItemWithCountsAndBooleans MapFromDomain(Domain.FoodItem foodItem, int userId)
        {
            var res = foodItem == null
                ? null
                : new DAL.App.DTO.FoodItemWithCountsAndBooleans()
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
                    FoodItemTags = foodItem.FoodItemTags.Select(FoodItemTagMapper.MapFromDomain).ToList(),
                    UserRating = foodItem.Ratings
                        .Where(rating => rating.AppUserId == userId)
                        .Sum(rating => rating.RatingValue),
                    HasUserMadeDepletedReport = foodItem.DepletedReports.Any(report => report.AppUserId == userId),
                };

            return res;
        }
    }
}