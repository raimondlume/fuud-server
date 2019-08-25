using me.raimondlu.Contracts.DAL.Base.Repositories;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProviderRepository : IProviderRepository<DALAppDTO.Provider>
    {
        
    }
    
    public interface IProviderRepository<TDALEntity> : IBaseRepository<TDALEntity> 
        where TDALEntity : class, new()
    {      
        
    }
}