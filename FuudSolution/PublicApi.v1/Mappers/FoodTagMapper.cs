using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class FoodTagMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.FoodTag))
            {
                return MapFromBLL((internalDTO.FoodTag) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.FoodTag))
            {
                return MapFromExternal((externalDTO.FoodTag) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.FoodTag MapFromBLL(internalDTO.FoodTag foodTag)
        {
            var res = foodTag == null ? null : new externalDTO.FoodTag
            {
                Id = foodTag.Id,
                FoodTagValue = foodTag.FoodTagValue
            };


            return res;
        }

        public static internalDTO.FoodTag MapFromExternal(externalDTO.FoodTag foodTag)
        {
            var res = foodTag == null ? null : new internalDTO.FoodTag
            {
                Id = foodTag.Id,
                FoodTagValue = foodTag.FoodTagValue
            };


            return res;
        }
    }
}