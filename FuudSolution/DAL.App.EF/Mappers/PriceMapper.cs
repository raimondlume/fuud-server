using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class PriceMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.Price))
            {
                return MapFromDomain((Domain.Price) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Price))
            {
                return MapFromDAL((DAL.App.DTO.Price) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.Price MapFromDomain(Domain.Price price)
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

        public static Domain.Price MapFromDAL(DAL.App.DTO.Price price)
        {
            var res = price == null ? null : new Domain.Price
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