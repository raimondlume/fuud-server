using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Helpers;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class CommentRepository : BaseRepository<DAL.App.DTO.Comment, Domain.Comment, AppDbContext>, ICommentRepository
    {
        public CommentRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new CommentMapper())
        {
        }

        public override async Task<List<DAL.App.DTO.Comment>> AllAsync()
        {
            return await RepositoryDbSet
                .Include(comment => comment.FoodItem)
                .Include(comment => comment.AppUser)
                .Select(comment => CommentMapper.MapFromDomain(comment))
                .ToListAsync();
        }
        
        public async Task<List<DAL.App.DTO.Comment>> AllForFoodItemAsync(int foodItemId)
        {
            return await RepositoryDbSet
                .Where(comment => comment.FoodItemId == foodItemId)
                .Include(comment => comment.AppUser)
                .Select(e => CommentMapper.MapFromDomain(e))
                .ToListAsync();
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet
                .AnyAsync(comment => comment.Id == id && comment.AppUserId == userId);
        }
    }
}