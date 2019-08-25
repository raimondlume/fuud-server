using System;
using System.Linq;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class FoodItemMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodItem))
            {
                return MapFromDomain((Domain.FoodItem) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.FoodItem))
            {
                return MapFromDAL((DAL.App.DTO.FoodItem) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.FoodItem MapFromDomain(Domain.FoodItem foodItem)
        {
            var res = foodItem == null ? null : new DAL.App.DTO.FoodItem
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromDomain(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromDomain(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst,
                Prices = foodItem.Prices?.Select(PriceMapper.MapFromDomain).ToList()
            };


            return res;
        }

        public static Domain.FoodItem MapFromDAL(DAL.App.DTO.FoodItem foodItem)
        {
            var res = foodItem == null ? null : new Domain.FoodItem
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromDAL(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromDAL(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst
            };


            return res;
        }
    }
}