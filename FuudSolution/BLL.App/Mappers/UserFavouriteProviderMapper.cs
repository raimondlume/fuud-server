using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class UserFavouriteProviderMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.UserFavouriteProvider))
            {
                return MapFromDAL((DAL.App.DTO.UserFavouriteProvider) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.UserFavouriteProvider))
            {
                return MapFromBLL((BLL.App.DTO.UserFavouriteProvider) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.UserFavouriteProvider MapFromDAL(DAL.App.DTO.UserFavouriteProvider userFavouriteProvider)
        {
            var res = userFavouriteProvider == null ? null : new BLL.App.DTO.UserFavouriteProvider
            {
                Id = userFavouriteProvider.Id,
                ProviderId = userFavouriteProvider.ProviderId,
                Provider = ProviderMapper.MapFromDAL(userFavouriteProvider.Provider),
                AppUserId = userFavouriteProvider.AppUserId
            };


            return res;
        }

        public static DAL.App.DTO.UserFavouriteProvider MapFromBLL(BLL.App.DTO.UserFavouriteProvider userFavouriteProvider)
        {
            var res = userFavouriteProvider == null ? null : new DAL.App.DTO.UserFavouriteProvider
            {
                Id = userFavouriteProvider.Id,
                ProviderId = userFavouriteProvider.ProviderId,
                Provider = ProviderMapper.MapFromBLL(userFavouriteProvider.Provider),
                AppUserId = userFavouriteProvider.AppUserId
            };


            return res;
        }
    }
}