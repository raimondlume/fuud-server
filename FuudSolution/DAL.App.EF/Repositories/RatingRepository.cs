using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class RatingRepository : BaseRepository<DAL.App.DTO.Rating, Domain.Rating, AppDbContext>, IRatingRepository
    {
        public RatingRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new RatingMapper())
        {
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet
                .AnyAsync(rating => rating.Id == id && rating.AppUserId == userId);
        }
    }
}