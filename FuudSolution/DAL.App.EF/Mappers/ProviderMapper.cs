using System;
using me.raimondlu.Contracts.DAL.Base.Mappers;
using DAL.App.DTO;

namespace DAL.App.EF.Mappers
{
    public class ProviderMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.Provider))
            {
                return MapFromDomain((Domain.Provider) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.Provider))
            {
                return MapFromDAL((DAL.App.DTO.Provider) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.Provider MapFromDomain(Domain.Provider provider)
        {
            var res = provider == null ? null : new DAL.App.DTO.Provider
            {
                Id = provider.Id,
                Address = provider.Address,
                Name = provider.Name,
                LocationLatitude = provider.LocationLatitude,
                LocationLongitude = provider.LocationLongitude
            };


            return res;
        }

        public static Domain.Provider MapFromDAL(DAL.App.DTO.Provider provider)
        {
            var res = provider == null ? null : new Domain.Provider
            {
                Id = provider.Id,
                Address = provider.Address,
                Name = provider.Name,
                LocationLatitude = provider.LocationLatitude,
                LocationLongitude = provider.LocationLongitude
            };


            return res;
        }
    }
}