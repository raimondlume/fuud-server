using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using me.raimondlu.DAL.Base.EF.Helpers;

namespace DAL.App.EF.Helpers
{
    public class AppRepositoryFactory : BaseRepositoryFactory<AppDbContext>
    {
        public AppRepositoryFactory()
        {
            RegisterRepositories();
        }

        private void RegisterRepositories()
        {
            AddToCreationMethods<ICommentRepository>(dataContext => new CommentRepository(dataContext));
            AddToCreationMethods<IFoodCategoryRepository>(dataContext => new FoodCategoryRepository(dataContext));
            AddToCreationMethods<IFoodItemRepository>(dataContext => new FoodItemRepository(dataContext));
            AddToCreationMethods<IFoodItemDepletedReportRepository>(dataContext => new FoodItemDepletedReportRepository(dataContext));
            AddToCreationMethods<IFoodItemTagRepository>(dataContext => new FoodItemTagRepository(dataContext));
            AddToCreationMethods<IFoodTagRepository>(dataContext => new FoodTagRepository(dataContext));
            AddToCreationMethods<IPriceRepository>(dataContext => new PriceRepository(dataContext));
            AddToCreationMethods<IProviderRepository>(dataContext => new ProviderRepository(dataContext));
            AddToCreationMethods<IRatingRepository>(dataContext => new RatingRepository(dataContext));
            AddToCreationMethods<IUserFavouriteProviderRepository>(dataContext => new UserFavouriteProviderRepository(dataContext));
        }
    }
}