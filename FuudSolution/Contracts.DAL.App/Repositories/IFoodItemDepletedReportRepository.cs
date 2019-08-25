using System.Collections.Generic;
using System.Threading.Tasks;
using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodItemDepletedReportRepository : IFoodItemDepletedReportRepository<DALAppDTO.FoodItemDepletedReport>
    {
    }

    public interface IFoodItemDepletedReportRepository<TDALEntity> : IBaseRepository<TDALEntity>
        where TDALEntity : class, new()
    {
        Task<bool> BelongsToUserAsync(int id, int userId);
    }
}