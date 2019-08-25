using System;
using externalDTO = PublicApi.v1.DTO;
using internalDTO = BLL.App.DTO;

namespace PublicApi.v1.Mappers
{
    public class ProviderMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(externalDTO.Provider))
            {
                return MapFromBLL((internalDTO.Provider) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(internalDTO.Provider))
            {
                return MapFromExternal((externalDTO.Provider) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static externalDTO.Provider MapFromBLL(internalDTO.Provider provider)
        {
            var res = provider == null ? null : new externalDTO.Provider
            {
                Id = provider.Id,
                Address = provider.Address,
                Name = provider.Name,
                LocationLatitude = provider.LocationLatitude,
                LocationLongitude = provider.LocationLongitude
            };


            return res;
        }

        public static internalDTO.Provider MapFromExternal(externalDTO.Provider provider)
        {
            var res = provider == null ? null : new internalDTO.Provider
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