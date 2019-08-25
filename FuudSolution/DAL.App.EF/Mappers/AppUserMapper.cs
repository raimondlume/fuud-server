using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class AppUserMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.Identity.AppUser))
            {
                return MapFromDomain((Domain.Identity.AppUser) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Identity.AppUser))
            {
                return MapFromDAL((DAL.App.DTO.Identity.AppUser) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.Identity.AppUser MapFromDomain(Domain.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new DAL.App.DTO.Identity.AppUser
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName
            };


            return res;
        }

        public static Domain.Identity.AppUser MapFromDAL(DAL.App.DTO.Identity.AppUser appUser)
        {
            var res = appUser == null ? null : new Domain.Identity.AppUser
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName
            };


            return res;
        }
    }
}