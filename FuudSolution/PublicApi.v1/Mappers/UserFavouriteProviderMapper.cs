using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class UserFavouriteProviderMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.UserFavouriteProvider))
            {
                return MapFromBLL((internalDTO.UserFavouriteProvider) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.UserFavouriteProvider))
            {
                return MapFromExternal((externalDTO.UserFavouriteProvider) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.UserFavouriteProvider MapFromBLL(internalDTO.UserFavouriteProvider userFavouriteProvider)
        {
            var res = userFavouriteProvider == null ? null : new externalDTO.UserFavouriteProvider
            {
                Id = userFavouriteProvider.Id,
                ProviderId = userFavouriteProvider.ProviderId,
                Provider = ProviderMapper.MapFromBLL(userFavouriteProvider.Provider),
                AppUserId = userFavouriteProvider.AppUserId
            };


            return res;
        }

        public static internalDTO.UserFavouriteProvider MapFromExternal(externalDTO.UserFavouriteProvider userFavouriteProvider)
        {
            var res = userFavouriteProvider == null ? null : new internalDTO.UserFavouriteProvider
            {
                Id = userFavouriteProvider.Id,
                ProviderId = userFavouriteProvider.ProviderId,
                Provider = ProviderMapper.MapFromExternal(userFavouriteProvider.Provider),
                AppUserId = userFavouriteProvider.AppUserId
            };


            return res;
        }
    }
}