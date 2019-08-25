using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using UserFavouriteProvider = BLL.App.DTO.UserFavouriteProvider;

namespace BLL.App.Services
{
    public class UserFavouriteProviderService :
        BaseEntityService<BLL.App.DTO.UserFavouriteProvider, DAL.App.DTO.UserFavouriteProvider, IAppUnitOfWork>,
        IUserFavouriteProviderService
    {
        public UserFavouriteProviderService(IAppUnitOfWork uow) : base(uow, new UserFavouriteProviderMapper())
        {
            ServiceRepository = Uow.UserFavouriteProviders;
        }

        public async Task<List<UserFavouriteProvider>> AllForUserAsync(int userId)
        {
            return (await Uow.UserFavouriteProviders.AllForUserAsync(userId)).Select(UserFavouriteProviderMapper.MapFromDAL).ToList();;
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await Uow.UserFavouriteProviders.BelongsToUserAsync(id, userId);
        }
    }
}