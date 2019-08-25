using System;
using me.raimondlu.Contracts.BLL.Base.Mappers;

namespace BLL.App.Mappers
{
    public class ProviderMapper : IBaseBLLMapper
    {
        public TOutObject Map<TOutObject>(object inObject)
            where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(BLL.App.DTO.Provider))
            {
                return MapFromDAL((DAL.App.DTO.Provider) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(DAL.App.DTO.Provider))
            {
                return MapFromBLL((BLL.App.DTO.Provider) inObject) as TOutObject;
            }

            throw new InvalidCastException($"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static BLL.App.DTO.Provider MapFromDAL(DAL.App.DTO.Provider provider)
        {
            var res = provider == null ? null : new BLL.App.DTO.Provider
            {
                Id = provider.Id,
                Address = provider.Address,
                Name = provider.Name,
                LocationLatitude = provider.LocationLatitude,
                LocationLongitude = provider.LocationLongitude
            };


            return res;
        }

        public static DAL.App.DTO.Provider MapFromBLL(BLL.App.DTO.Provider provider)
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
    }
}