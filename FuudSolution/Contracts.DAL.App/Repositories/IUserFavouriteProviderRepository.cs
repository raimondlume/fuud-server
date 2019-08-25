using System.Collections.Generic;
using System.Threading.Tasks;
using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IUserFavouriteProviderRepository : IUserFavouriteProviderRepository<DALAppDTO.UserFavouriteProvider>
    {
        
    }
    
    public interface IUserFavouriteProviderRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {      
        Task<List<TDALEntity>> AllForUserAsync(int userId);
        Task<bool> BelongsToUserAsync(int id, int userId);
    }
}