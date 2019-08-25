using System.Threading.Tasks;
using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;

namespace BLL.App.Services
{
    public class RatingService : BaseEntityService<BLL.App.DTO.Rating, DAL.App.DTO.Rating, IAppUnitOfWork>,
        IRatingService
    {
        public RatingService(IAppUnitOfWork uow) : base(uow, new RatingMapper())
        {
            ServiceRepository = Uow.Ratings;
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await Uow.Ratings.BelongsToUserAsync(id, userId);
        }
    }
}