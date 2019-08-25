using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FoodCategoryMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.FoodCategory))
            {
                return MapFromDAL((DAL.App.DTO.FoodCategory) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodCategory))
            {
                return MapFromBLL((BLL.App.DTO.FoodCategory) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.FoodCategory MapFromDAL(DAL.App.DTO.FoodCategory foodCategory)
        {
            var res = foodCategory == null ? null : new BLL.App.DTO.FoodCategory
            {
                Id = foodCategory.Id,
                FoodCategoryValue = foodCategory.FoodCategoryValue
            };


            return res;
        }

        public static DAL.App.DTO.FoodCategory MapFromBLL(BLL.App.DTO.FoodCategory foodCategory)
        {
            var res = foodCategory == null ? null : new DAL.App.DTO.FoodCategory
            {
                Id = foodCategory.Id,
                FoodCategoryValue = foodCategory.FoodCategoryValue
            };


            return res;
        }
    }
}