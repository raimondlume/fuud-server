using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class RatingMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.Rating))
            {
                return MapFromDomain((Domain.Rating) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Rating))
            {
                return MapFromDAL((DAL.App.DTO.Rating) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.Rating MapFromDomain(Domain.Rating rating)
        {
            var res = rating == null ? null : new DAL.App.DTO.Rating
            {
                Id = rating.Id,
                Timestamp = rating.Timestamp,
                RatingValue = rating.RatingValue,
                FoodItemId = rating.FoodItemId,
                AppUserId = rating.AppUserId
            };


            return res;
        }

        public static Domain.Rating MapFromDAL(DAL.App.DTO.Rating rating)
        {
            var res = rating == null ? null : new Domain.Rating
            {
                Id = rating.Id,
                Timestamp = rating.Timestamp,
                RatingValue = rating.RatingValue,
                FoodItemId = rating.FoodItemId,
                AppUserId = rating.AppUserId
            };


            return res;
        }
    }
}