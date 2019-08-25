using System;
using System.Linq;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class FoodItemMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.FoodItem))
            {
                return MapFromBLL((internalDTO.FoodItem) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.FoodItem))
            {
                return MapFromExternal((externalDTO.FoodItem) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.FoodItem MapFromBLL(internalDTO.FoodItem foodItem)
        {
            var res = foodItem == null ? null : new externalDTO.FoodItem
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromBLL(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromBLL(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst,
                Prices = foodItem.Prices?.Select(PriceMapper.MapFromBLL).ToList()
            };


            return res;
        }

        public static internalDTO.FoodItem MapFromExternal(externalDTO.FoodItem foodItem)
        {
            var res = foodItem == null ? null : new internalDTO.FoodItem
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromExternal(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromExternal(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst
            };


            return res;
        }
    }
}