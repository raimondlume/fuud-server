using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FoodItemRepository : BaseRepository<DAL.App.DTO.FoodItem, Domain.FoodItem, AppDbContext>,
        IFoodItemRepository
    {
        public FoodItemRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new FoodItemMapper())
        {
        }

        public override async Task<List<DAL.App.DTO.FoodItem>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(item => item.Provider)
                .Include(item => item.FoodCategory)
                .Include(item => item.Prices)
                .Select(item => FoodItemMapper.MapFromDomain(item))
                .ToListAsync();
        }

        public async Task<List<DAL.App.DTO.FoodItemWithCounts>> AllActiveWithCountsAsync()
        {
            return await RepositoryDbSet
                .Where(item => item.DateStart.Day == DateTime.Now.Day)
                .Where(item => item.DateEnd == null || item.DateEnd > DateTime.Now)
                .Include(item => item.Provider)
                .Include(item => item.FoodCategory)
                .Include(item => item.Prices)
                .Include(item => item.Comments)
                .Include(item => item.Ratings)
                .Include(item => item.FoodItemTags)
                .ThenInclude(tag => tag.FoodTag)
                .Include(item => item.DepletedReports)
                .Select(item => FoodItemWithCountsMapper.MapFromDomain(item))
                .ToListAsync();
        }

        public async Task<List<FoodItemWithCountsAndBooleans>> AllActiveWithCountsAndBooleansAsync(int userId)
        {
            return await RepositoryDbSet
                .Where(item => item.DateStart.Day == DateTime.Now.Day)
                .Where(item => item.DateEnd == null || item.DateEnd > DateTime.Now)
                .Include(item => item.Provider)
                .Include(item => item.FoodCategory)
                .Include(item => item.Prices)
                .Include(item => item.Comments)
                .Include(item => item.Ratings)
                .Include(item => item.FoodItemTags)
                .ThenInclude(tag => tag.FoodTag)
                .Include(item => item.DepletedReports)
                .Select(item => FoodItemWithCountsAndBooleansMapper.MapFromDomain(item, userId))
                .ToListAsync();
        }

        public void ArchiveAllActiveFoodItemsFromProvider(int providerId)
        {
            var entitiesToRemove = RepositoryDbSet
                .Where(item => item.ProviderId == providerId)
                .Where(item => item.DateStart.Day == DateTime.Now.Day)
                .ToList();

            foreach (var foodItem in entitiesToRemove)
            {
                foodItem.DateEnd = DateTime.Now;
            }

            RepositoryDbSet.UpdateRange(entitiesToRemove);
        }
    }
}