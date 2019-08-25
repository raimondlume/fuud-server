using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FoodItemTagMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.FoodItemTag))
            {
                return MapFromDAL((DAL.App.DTO.FoodItemTag) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodItemTag))
            {
                return MapFromBLL((BLL.App.DTO.FoodItemTag) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.FoodItemTag MapFromDAL(DAL.App.DTO.FoodItemTag foodItemTag)
        {
            var res = foodItemTag == null ? null : new BLL.App.DTO.FoodItemTag
            {
                Id = foodItemTag.Id,
                FoodItem = FoodItemMapper.MapFromDAL(foodItemTag.FoodItem),
                FoodItemId = foodItemTag.FoodItemId,
                FoodTag = FoodTagMapper.MapFromDAL(foodItemTag.FoodTag),
                FoodTagId = foodItemTag.FoodTagId
            };


            return res;
        }

        public static DAL.App.DTO.FoodItemTag MapFromBLL(BLL.App.DTO.FoodItemTag foodItemTag)
        {
            var res = foodItemTag == null ? null : new DAL.App.DTO.FoodItemTag
            {
                Id = foodItemTag.Id,
                FoodItem = FoodItemMapper.MapFromBLL(foodItemTag.FoodItem),
                FoodItemId = foodItemTag.FoodItemId,
                FoodTag = FoodTagMapper.MapFromBLL(foodItemTag.FoodTag),
                FoodTagId = foodItemTag.FoodTagId
            };


            return res;
        }
    }
}