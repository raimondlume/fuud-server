using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class FoodItemDepletedReportMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.FoodItemDepletedReport))
            {
                return MapFromDAL((DAL.App.DTO.FoodItemDepletedReport) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.FoodItemDepletedReport))
            {
                return MapFromBLL((BLL.App.DTO.FoodItemDepletedReport) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.FoodItemDepletedReport MapFromDAL(DAL.App.DTO.FoodItemDepletedReport foodItemDepletedReport)
        {
            var res = foodItemDepletedReport == null ? null : new BLL.App.DTO.FoodItemDepletedReport
            {
                Id = foodItemDepletedReport.Id,
                Timestamp = foodItemDepletedReport.Timestamp,
                FoodItem = FoodItemMapper.MapFromDAL(foodItemDepletedReport.FoodItem),
                FoodItemId = foodItemDepletedReport.FoodItemId,
                AppUserId = foodItemDepletedReport.AppUserId
            };


            return res;
        }

        public static DAL.App.DTO.FoodItemDepletedReport MapFromBLL(BLL.App.DTO.FoodItemDepletedReport foodItemDepletedReport)
        {
            var res = foodItemDepletedReport == null ? null : new DAL.App.DTO.FoodItemDepletedReport
            {
                Id = foodItemDepletedReport.Id,
                Timestamp = foodItemDepletedReport.Timestamp,
                FoodItem = FoodItemMapper.MapFromBLL(foodItemDepletedReport.FoodItem),
                FoodItemId = foodItemDepletedReport.FoodItemId,
                AppUserId = foodItemDepletedReport.AppUserId
            };


            return res;
        }
    }
}