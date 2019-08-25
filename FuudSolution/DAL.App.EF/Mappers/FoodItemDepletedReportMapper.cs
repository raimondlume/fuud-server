using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class FoodItemDepletedReportMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodItemDepletedReport))
            {
                return MapFromDomain((Domain.FoodItemDepletedReport) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.FoodItemDepletedReport))
            {
                return MapFromDAL((DAL.App.DTO.FoodItemDepletedReport) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.FoodItemDepletedReport MapFromDomain(Domain.FoodItemDepletedReport foodItemDepletedReport)
        {
            var res = foodItemDepletedReport == null ? null : new DAL.App.DTO.FoodItemDepletedReport
            {
                Id = foodItemDepletedReport.Id,
                Timestamp = foodItemDepletedReport.Timestamp,
                FoodItemId = foodItemDepletedReport.FoodItemId,
                AppUserId = foodItemDepletedReport.AppUserId
            };


            return res;
        }

        public static Domain.FoodItemDepletedReport MapFromDAL(DAL.App.DTO.FoodItemDepletedReport foodItemDepletedReport)
        {
            var res = foodItemDepletedReport == null ? null : new Domain.FoodItemDepletedReport
            {
                Id = foodItemDepletedReport.Id,
                Timestamp = foodItemDepletedReport.Timestamp,
                FoodItemId = foodItemDepletedReport.FoodItemId,
                AppUserId = foodItemDepletedReport.AppUserId
            };


            return res;
        }
    }
}