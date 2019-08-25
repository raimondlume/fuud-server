using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FoodItemDepletedReportRepository :
        BaseRepository<DAL.App.DTO.FoodItemDepletedReport, Domain.FoodItemDepletedReport, AppDbContext>,
        IFoodItemDepletedReportRepository
    {
        public FoodItemDepletedReportRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext,
            new FoodItemDepletedReportMapper())
        {
        }
        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet
                .AnyAsync(foodItemDepletedReport => foodItemDepletedReport.Id == id 
                                                    && foodItemDepletedReport.AppUserId == userId);
        }
    }
}