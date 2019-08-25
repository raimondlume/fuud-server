using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class PriceMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.Price))
            {
                return MapFromDAL((DAL.App.DTO.Price) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.Price))
            {
                return MapFromBLL((BLL.App.DTO.Price) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.Price MapFromDAL(DAL.App.DTO.Price price)
        {
            var res = price == null ? null : new BLL.App.DTO.Price
            {
                Id = price.Id,
                PriceValue = price.PriceValue,
                FoodItemId = price.FoodItemId,
                ModifierName = price.ModifierName
            };


            return res;
        }

        public static DAL.App.DTO.Price MapFromBLL(BLL.App.DTO.Price price)
        {
            var res = price == null ? null : new DAL.App.DTO.Price
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