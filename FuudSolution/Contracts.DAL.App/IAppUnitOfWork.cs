using Contracts.DAL.App.Repositories;
using me.raimondlu.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        ICommentRepository Comments { get; }
        IFoodCategoryRepository FoodCategories { get; }
        IFoodItemDepletedReportRepository FoodItemDepletedReports { get; }
        IFoodItemRepository FoodItems { get; }
        IFoodItemTagRepository FoodItemTags { get; }
        IFoodTagRepository FoodTags { get; }
        IPriceRepository Prices { get; }
        IProviderRepository Providers { get; }
        IRatingRepository Ratings { get; }
        IUserFavouriteProviderRepository UserFavouriteProviders { get; }
    }
}