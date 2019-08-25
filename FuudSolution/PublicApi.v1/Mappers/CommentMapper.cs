using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class CommentMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Comment))
            {
                return MapFromBLL((internalDTO.Comment) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Comment))
            {
                return MapFromExternal((externalDTO.Comment) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.Comment MapFromBLL(internalDTO.Comment comment)
        {
            var res = comment == null ? null : new externalDTO.Comment
            {
                Id = comment.Id,
                Timestamp = comment.Timestamp,
                CommentValue = comment.CommentValue,
                FoodItemId = comment.FoodItemId,
                AppUserId = comment.AppUserId,
                AppUser = AppUserMapper.MapFromBLL(comment.AppUser)
            };


            return res;
        }

        public static internalDTO.Comment MapFromExternal(externalDTO.Comment comment)
        {
            var res = comment == null ? null : new internalDTO.Comment
            {
                Id = comment.Id,
                Timestamp = DateTime.Now,
                CommentValue = comment.CommentValue,
                FoodItemId = comment.FoodItemId,
                AppUserId = comment.AppUserId,
                AppUser = AppUserMapper.MapFromExternal(comment.AppUser)
            };


            return res;
        }
    }
}