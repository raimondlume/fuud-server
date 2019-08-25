using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;

namespace BLL.App.Services
{
    public class FoodItemDepletedReportService : BaseEntityService<BLL.App.DTO.FoodItemDepletedReport, DAL.App.DTO.FoodItemDepletedReport, IAppUnitOfWork>,
        IFoodItemDepletedReportService
    {
        public FoodItemDepletedReportService(IAppUnitOfWork uow) : base(uow, new FoodItemDepletedReportMapper())
        {
            ServiceRepository = Uow.FoodItemDepletedReports;
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await Uow.FoodItemDepletedReports.BelongsToUserAsync(id, userId);
        }
    }
}