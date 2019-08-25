using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PriceRepository : BaseRepository<DAL.App.DTO.Price, Domain.Price, AppDbContext>, IPriceRepository
    {
        public PriceRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new PriceMapper())
        {
        }

        public override async Task<List<DAL.App.DTO.Price>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(price => price.FoodItem)
                .Select(price => PriceMapper.MapFromDomain(price))
                .ToListAsync();
        }
        
        public async Task<List<DAL.App.DTO.Price>> AllForFoodItemAsync(int foodItemId)
        {
            return await RepositoryDbSet
                .Where(price => price.FoodItemId == foodItemId)
                .Select(e => PriceMapper.MapFromDomain(e))
                .ToListAsync();
        }
    }
}