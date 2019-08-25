using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using me.raimondlu.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProviderRepository : BaseRepository<DAL.App.DTO.Provider, Domain.Provider, AppDbContext>, IProviderRepository
    {
        public ProviderRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ProviderMapper())
        {
        }
    }
}