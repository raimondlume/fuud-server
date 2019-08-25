using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class FoodItemTagMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.FoodItemTag))
            {
                return MapFromBLL((internalDTO.FoodItemTag) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.FoodItemTag))
            {
                return MapFromExternal((externalDTO.FoodItemTag) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.FoodItemTag MapFromBLL(internalDTO.FoodItemTag foodItemTag)
        {
            var res = foodItemTag == null ? null : new externalDTO.FoodItemTag
            {
                Id = foodItemTag.Id,
                FoodItemId = foodItemTag.FoodItemId,
                FoodTag = FoodTagMapper.MapFromBLL(foodItemTag.FoodTag),
                FoodTagId = foodItemTag.FoodTagId
            };


            return res;
        }

        public static internalDTO.FoodItemTag MapFromExternal(externalDTO.FoodItemTag foodItemTag)
        {
            var res = foodItemTag == null ? null : new internalDTO.FoodItemTag
            {
                Id = foodItemTag.Id,
                FoodItemId = foodItemTag.FoodItemId,
                FoodTag = FoodTagMapper.MapFromExternal(foodItemTag.FoodTag),
                FoodTagId = foodItemTag.FoodTagId
            };


            return res;
        }
    }
}