using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class FoodCategoryMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodCategory))
            {
                return MapFromDomain((Domain.FoodCategory) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.FoodCategory))
            {
                return MapFromDAL((DAL.App.DTO.FoodCategory) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.FoodCategory MapFromDomain(Domain.FoodCategory foodCategory)
        {
            var res = foodCategory == null ? null : new DAL.App.DTO.FoodCategory
            {
                Id = foodCategory.Id,
                FoodCategoryValue = foodCategory.FoodCategoryValue
            };


            return res;
        }

        public static Domain.FoodCategory MapFromDAL(DAL.App.DTO.FoodCategory foodCategory)
        {
            var res = foodCategory == null ? null : new Domain.FoodCategory
            {
                Id = foodCategory.Id,
                FoodCategoryValue = foodCategory.FoodCategoryValue
            };


            return res;
        }
    }
}