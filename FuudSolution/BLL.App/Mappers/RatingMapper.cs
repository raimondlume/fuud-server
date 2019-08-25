using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class RatingMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.Rating))
            {
                return MapFromDAL((DAL.App.DTO.Rating) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.Rating))
            {
                return MapFromBLL((BLL.App.DTO.Rating) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.Rating MapFromDAL(DAL.App.DTO.Rating rating)
        {
            var res = rating == null ? null : new BLL.App.DTO.Rating
            {
                Id = rating.Id,
                Timestamp = rating.Timestamp,
                RatingValue = rating.RatingValue,
                FoodItem = FoodItemMapper.MapFromDAL(rating.FoodItem),
                FoodItemId = rating.FoodItemId,
                AppUserId = rating.AppUserId
            };


            return res;
        }

        public static DAL.App.DTO.Rating MapFromBLL(BLL.App.DTO.Rating rating)
        {
            var res = rating == null ? null : new DAL.App.DTO.Rating
            {
                Id = rating.Id,
                Timestamp = rating.Timestamp,
                RatingValue = rating.RatingValue,
                FoodItem = FoodItemMapper.MapFromBLL(rating.FoodItem),
                FoodItemId = rating.FoodItemId,
                AppUserId = rating.AppUserId
            };


            return res;
        }
    }
}