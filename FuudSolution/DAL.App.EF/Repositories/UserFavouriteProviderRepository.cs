using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class UserFavouriteProviderRepository :
        BaseRepository<DAL.App.DTO.UserFavouriteProvider, Domain.UserFavouriteProvider, AppDbContext>,
        IUserFavouriteProviderRepository
    {
        public UserFavouriteProviderRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext,
            new UserFavouriteProviderMapper())
        {
        }

        public async Task<List<DAL.App.DTO.UserFavouriteProvider>> AllForUserAsync(int userId)
        {
            return await RepositoryDbSet
                .Where(userFavouriteProvider => userFavouriteProvider.AppUserId == userId)
                .Select(e => UserFavouriteProviderMapper.MapFromDomain(e))
                .ToListAsync();
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await RepositoryDbSet
                .AnyAsync(userFavouriteProvider =>
                    userFavouriteProvider.Id == id && userFavouriteProvider.AppUserId == userId);
        }
    }
}