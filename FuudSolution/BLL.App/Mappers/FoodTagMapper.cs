using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FoodTagMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.FoodTag))
            {
                return MapFromDAL((DAL.App.DTO.FoodTag) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodTag))
            {
                return MapFromBLL((BLL.App.DTO.FoodTag) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.FoodTag MapFromDAL(DAL.App.DTO.FoodTag foodTag)
        {
            var res = foodTag == null ? null : new BLL.App.DTO.FoodTag
            {
                Id = foodTag.Id,
                FoodTagValue = foodTag.FoodTagValue
            };


            return res;
        }

        public static DAL.App.DTO.FoodTag MapFromBLL(BLL.App.DTO.FoodTag foodTag)
        {
            var res = foodTag == null ? null : new DAL.App.DTO.FoodTag
            {
                Id = foodTag.Id,
                FoodTagValue = foodTag.FoodTagValue
            };


            return res;
        }
    }
}