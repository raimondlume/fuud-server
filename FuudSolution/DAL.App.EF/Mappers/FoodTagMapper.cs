using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class FoodTagMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodTag))
            {
                return MapFromDomain((Domain.FoodTag) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.FoodTag))
            {
                return MapFromDAL((DAL.App.DTO.FoodTag) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.FoodTag MapFromDomain(Domain.FoodTag foodTag)
        {
            var res = foodTag == null ? null : new DAL.App.DTO.FoodTag
            {
                Id = foodTag.Id,
                FoodTagValue = foodTag.FoodTagValue
            };


            return res;
        }

        public static Domain.FoodTag MapFromDAL(DAL.App.DTO.FoodTag foodTag)
        {
            var res = foodTag == null ? null : new Domain.FoodTag
            {
                Id = foodTag.Id,
                FoodTagValue = foodTag.FoodTagValue
            };


            return res;
        }
    }
}