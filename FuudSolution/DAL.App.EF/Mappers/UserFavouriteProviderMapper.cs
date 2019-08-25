using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class UserFavouriteProviderMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.UserFavouriteProvider))
            {
                return MapFromDomain((Domain.UserFavouriteProvider) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.UserFavouriteProvider))
            {
                return MapFromDAL((DAL.App.DTO.UserFavouriteProvider) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.UserFavouriteProvider MapFromDomain(Domain.UserFavouriteProvider userFavouriteProvider)
        {
            var res = userFavouriteProvider == null ? null : new DAL.App.DTO.UserFavouriteProvider
            {
                Id = userFavouriteProvider.Id,
                ProviderId = userFavouriteProvider.ProviderId,
                Provider = ProviderMapper.MapFromDomain(userFavouriteProvider.Provider),
                AppUserId = userFavouriteProvider.AppUserId
            };


            return res;
        }

        public static Domain.UserFavouriteProvider MapFromDAL(DAL.App.DTO.UserFavouriteProvider userFavouriteProvider)
        {
            var res = userFavouriteProvider == null ? null : new Domain.UserFavouriteProvider
            {
                Id = userFavouriteProvider.Id,
                ProviderId = userFavouriteProvider.ProviderId,
                Provider = ProviderMapper.MapFromDAL(userFavouriteProvider.Provider),
                AppUserId = userFavouriteProvider.AppUserId
            };


            return res;
        }
    }
}