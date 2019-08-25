using System.Collections.Generic;
using System.Threading.Tasks;
using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPriceRepository : IPriceRepository<DALAppDTO.Price>
    {
    }

    public interface IPriceRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<List<TDALEntity>> AllForFoodItemAsync(int foodItemId);
    }
}