using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;
using BLL.App.Mappers;

namespace BLL.App.Mappers
{
    public class CommentMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.Comment))
            {
                return MapFromDAL((DAL.App.DTO.Comment) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.Comment))
            {
                return MapFromBLL((BLL.App.DTO.Comment) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.Comment MapFromDAL(DAL.App.DTO.Comment comment)
        {
            var res = comment == null ? null : new BLL.App.DTO.Comment
            {
                Id = comment.Id,
                Timestamp = comment.Timestamp,
                CommentValue = comment.CommentValue,
                FoodItem = FoodItemMapper.MapFromDAL(comment.FoodItem),
                FoodItemId = comment.FoodItemId,
                AppUserId = comment.AppUserId,
                AppUser = AppUserMapper.MapFromDAL(comment.AppUser)
            };


            return res;
        }

        public static DAL.App.DTO.Comment MapFromBLL(BLL.App.DTO.Comment comment)
        {
            var res = comment == null ? null : new DAL.App.DTO.Comment
            {
                Id = comment.Id,
                Timestamp = comment.Timestamp,
                CommentValue = comment.CommentValue,
                FoodItem = FoodItemMapper.MapFromBLL(comment.FoodItem),
                FoodItemId = comment.FoodItemId,
                AppUserId = comment.AppUserId,
                AppUser = AppUserMapper.MapFromBLL(comment.AppUser)
            };


            return res;
        }
    }
}