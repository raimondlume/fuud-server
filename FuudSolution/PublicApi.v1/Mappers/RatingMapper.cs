using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class RatingMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Rating))
            {
                return MapFromBLL((internalDTO.Rating) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Rating))
            {
                return MapFromExternal((externalDTO.Rating) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.Rating MapFromBLL(internalDTO.Rating rating)
        {
            var res = rating == null ? null : new externalDTO.Rating
            {
                Id = rating.Id,
                Timestamp = rating.Timestamp,
                RatingValue = rating.RatingValue,
                FoodItemId = rating.FoodItemId,
                AppUserId = rating.AppUserId
            };


            return res;
        }

        public static internalDTO.Rating MapFromExternal(externalDTO.Rating rating)
        {
            var res = rating == null ? null : new internalDTO.Rating
            {
                Id = rating.Id,
                Timestamp = DateTime.Now,
                RatingValue = rating.RatingValue,
                FoodItemId = rating.FoodItemId,
                AppUserId = rating.AppUserId
            };


            return res;
        }
    }
}