using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class AppUserMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Identity.AppUser))
            {
                return MapFromBLL((internalDTO.Identity.AppUser) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Identity.AppUser))
            {
                return MapFromExternal((externalDTO.Identity.AppUser) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.Identity.AppUser MapFromBLL(internalDTO.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new externalDTO.Identity.AppUser
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName
            };


            return res;
        }

        public static internalDTO.Identity.AppUser MapFromExternal(externalDTO.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new internalDTO.Identity.AppUser
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName
            };


            return res;
        }
    }
}