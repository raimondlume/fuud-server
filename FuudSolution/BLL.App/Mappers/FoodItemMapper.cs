using System;
using System.Linq;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FoodItemMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.FoodItem))
            {
                return MapFromDAL((DAL.App.DTO.FoodItem) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodItem))
            {
                return MapFromBLL((BLL.App.DTO.FoodItem) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.FoodItem MapFromDAL(DAL.App.DTO.FoodItem foodItem)
        {
            var res = foodItem == null ? null : new BLL.App.DTO.FoodItem
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromDAL(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromDAL(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst,
                Prices = foodItem.Prices?.Select(PriceMapper.MapFromDAL).ToList()
            };


            return res;
        }

        public static DAL.App.DTO.FoodItem MapFromBLL(BLL.App.DTO.FoodItem foodItem)
        {
            var res = foodItem == null ? null : new DAL.App.DTO.FoodItem
            {
                Id = foodItem.Id,
                ProviderId = foodItem.ProviderId,
                Provider = ProviderMapper.MapFromBLL(foodItem.Provider),
                DateStart = foodItem.DateStart,
                DateEnd = foodItem.DateEnd,
                FoodCategoryId = foodItem.FoodCategoryId,
                FoodCategory = FoodCategoryMapper.MapFromBLL(foodItem.FoodCategory),
                NameEng = foodItem.NameEng,
                NameEst = foodItem.NameEst
            };


            return res;
        }
    }
}