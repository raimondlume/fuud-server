using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FoodTagRepository : BaseRepository<DAL.App.DTO.FoodTag, Domain.FoodTag, AppDbContext>, IFoodTagRepository
    {
        public FoodTagRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new FoodTagMapper())
        {
        }
    }
}