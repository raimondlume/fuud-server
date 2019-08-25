using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class PriceMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Price))
            {
                return MapFromBLL((internalDTO.Price) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Price))
            {
                return MapFromExternal((externalDTO.Price) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.Price MapFromBLL(internalDTO.Price price)
        {
            var res = price == null ? null : new externalDTO.Price
            {
                Id = price.Id,
                PriceValue = price.PriceValue,
                FoodItemId = price.FoodItemId,
                ModifierName = price.ModifierName
            };


            return res;
        }

        public static internalDTO.Price MapFromExternal(externalDTO.Price price)
        {
            var res = price == null ? null : new internalDTO.Price
            {
                Id = price.Id,
                PriceValue = price.PriceValue,
                FoodItemId = price.FoodItemId,
                ModifierName = price.ModifierName
            };


            return res;
        }
    }
}