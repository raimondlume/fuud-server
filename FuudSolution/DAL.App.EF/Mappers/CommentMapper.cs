using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class CommentMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.Comment))
            {
                return MapFromDomain((Domain.Comment) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Comment))
            {
                return MapFromDAL((DAL.App.DTO.Comment) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.Comment MapFromDomain(Domain.Comment comment)
        {
            var res = comment == null ? null : new DAL.App.DTO.Comment
            {
                Id = comment.Id,
                Timestamp = comment.Timestamp,
                CommentValue = comment.CommentValue,
                FoodItemId = comment.FoodItemId,
                AppUserId = comment.AppUserId,
                AppUser = AppUserMapper.MapFromDomain(comment.AppUser)
            };


            return res;
        }

        public static Domain.Comment MapFromDAL(DAL.App.DTO.Comment comment)
        {
            var res = comment == null ? null : new Domain.Comment
            {
                Id = comment.Id,
                Timestamp = comment.Timestamp,
                CommentValue = comment.CommentValue,
                FoodItemId = comment.FoodItemId,
                AppUserId = comment.AppUserId
            };


            return res;
        }
    }
}