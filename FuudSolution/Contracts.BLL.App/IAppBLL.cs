using System;
using System.Threading.Tasks;
using Contracts.BLL.App.Services;
using me.raimondlu.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        ICommentService Comments { get; }
        IFoodCategoryService FoodCategories { get; }
        IFoodItemDepletedReportService FoodItemDepletedReports { get; }
        IFoodItemService FoodItems { get; }
        IFoodItemTagService FoodItemTags { get; }
        IFoodTagService FoodTags { get; }
        IPriceService Prices { get; }
        IProviderService Providers { get; }
        IRatingService Ratings { get; }
        IUserFavouriteProviderService UserFavouriteProviders { get; }
    }
}