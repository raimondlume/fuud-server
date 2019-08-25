using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class FoodItemDepletedReportMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.FoodItemDepletedReport))
            {
                return MapFromBLL((internalDTO.FoodItemDepletedReport) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.FoodItemDepletedReport))
            {
                return MapFromExternal((externalDTO.FoodItemDepletedReport) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.FoodItemDepletedReport MapFromBLL(internalDTO.FoodItemDepletedReport foodItemDepletedReport)
        {
            var res = foodItemDepletedReport == null ? null : new externalDTO.FoodItemDepletedReport
            {
                Id = foodItemDepletedReport.Id,
                Timestamp = foodItemDepletedReport.Timestamp,
                FoodItemId = foodItemDepletedReport.FoodItemId,
                AppUserId = foodItemDepletedReport.AppUserId
            };


            return res;
        }

        public static internalDTO.FoodItemDepletedReport MapFromExternal(externalDTO.FoodItemDepletedReport foodItemDepletedReport)
        {
            var res = foodItemDepletedReport == null ? null : new internalDTO.FoodItemDepletedReport
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