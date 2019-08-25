using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class FoodCategoryMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.FoodCategory))
            {
                return MapFromBLL((internalDTO.FoodCategory) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.FoodCategory))
            {
                return MapFromExternal((externalDTO.FoodCategory) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.FoodCategory MapFromBLL(internalDTO.FoodCategory foodCategory)
        {
            var res = foodCategory == null ? null : new externalDTO.FoodCategory
            {
                Id = foodCategory.Id,
                FoodCategoryValue = foodCategory.FoodCategoryValue
            };


            return res;
        }

        public static internalDTO.FoodCategory MapFromExternal(externalDTO.FoodCategory foodCategory)
        {
            var res = foodCategory == null ? null : new internalDTO.FoodCategory
            {
                Id = foodCategory.Id,
                FoodCategoryValue = foodCategory.FoodCategoryValue
            };


            return res;
        }
    }
}