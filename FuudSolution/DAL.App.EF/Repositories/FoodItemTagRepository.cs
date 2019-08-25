using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FoodItemTagRepository : BaseRepository<DAL.App.DTO.FoodItemTag, Domain.FoodItemTag, AppDbContext>, IFoodItemTagRepository    
    {
        public FoodItemTagRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new FoodItemTagMapper())
        {
        }
    }
}