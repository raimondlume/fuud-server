using System.Threading.Tasks;
using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRatingRepository : IRatingRepository<DALAppDTO.Rating>
    {
        
    }
    
    public interface IRatingRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {      
        Task<bool> BelongsToUserAsync(int id, int userId);
    }
}