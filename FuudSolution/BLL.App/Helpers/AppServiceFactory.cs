using BLL.App.Services;
using me.raimondlu.BLL.Base.Helpers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Helpers
{
    public class AppServiceFactory : BaseServiceFactory<IAppUnitOfWork>
    {
        public AppServiceFactory()
        {
            RegisterServices();
        }

        private void RegisterServices()
        {
            AddToCreationMethods<ICommentService>(uow => new CommentService(uow));
            AddToCreationMethods<IFoodCategoryService>(uow => new FoodCategoryService(uow));
            AddToCreationMethods<IFoodItemService>(uow => new FoodItemService(uow));
            AddToCreationMethods<IFoodItemDepletedReportService>(uow => new FoodItemDepletedReportService(uow));
            AddToCreationMethods<IFoodItemTagService>(uow => new FoodItemTagService(uow));
            AddToCreationMethods<IFoodTagService>(uow => new FoodTagService(uow));
            AddToCreationMethods<IPriceService>(uow => new PriceService(uow));
            AddToCreationMethods<IProviderService>(uow => new ProviderService(uow));
            AddToCreationMethods<IRatingService>(uow => new RatingService(uow));
            AddToCreationMethods<IUserFavouriteProviderService>(uow => new UserFavouriteProviderService(uow));
        }

    }

}