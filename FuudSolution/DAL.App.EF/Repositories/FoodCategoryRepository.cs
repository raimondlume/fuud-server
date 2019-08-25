using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FoodCategoryRepository : BaseRepository<DAL.App.DTO.FoodCategory, Domain.FoodCategory, AppDbContext>,
        IFoodCategoryRepository
    {
        public FoodCategoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext,
            new FoodCategoryMapper())
        {
        }
    }
}