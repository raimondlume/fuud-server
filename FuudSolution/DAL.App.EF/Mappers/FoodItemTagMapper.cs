using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class FoodItemTagMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodItemTag))
            {
                return MapFromDomain((Domain.FoodItemTag) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.FoodItemTag))
            {
                return MapFromDAL((DAL.App.DTO.FoodItemTag) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.FoodItemTag MapFromDomain(Domain.FoodItemTag foodItemTag)
        {
            var res = foodItemTag == null ? null : new DAL.App.DTO.FoodItemTag
            {
                Id = foodItemTag.Id,
                FoodItemId = foodItemTag.FoodItemId,
                FoodTag = FoodTagMapper.MapFromDomain(foodItemTag.FoodTag),
                FoodTagId = foodItemTag.FoodTagId
            };


            return res;
        }

        public static Domain.FoodItemTag MapFromDAL(DAL.App.DTO.FoodItemTag foodItemTag)
        {
            var res = foodItemTag == null ? null : new Domain.FoodItemTag
            {
                Id = foodItemTag.Id,
                FoodItemId = foodItemTag.FoodItemId,
                FoodTag = FoodTagMapper.MapFromDAL(foodItemTag.FoodTag),
                FoodTagId = foodItemTag.FoodTagId
            };


            return res;
        }
    }
}