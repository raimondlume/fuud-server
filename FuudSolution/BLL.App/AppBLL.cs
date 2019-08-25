using System;
using me.raimondlu.BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using me.raimondlu.Contracts.BLL.Base.Helpers;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected readonly IAppUnitOfWork AppUnitOfWork;
        
        public AppBLL(IAppUnitOfWork appUnitOfWork, IBaseServiceProvider serviceProvider) : base(appUnitOfWork, serviceProvider)
        {
            AppUnitOfWork = appUnitOfWork;
        }

        public ICommentService Comments => ServiceProvider.GetService<ICommentService>();
        public IFoodCategoryService FoodCategories => ServiceProvider.GetService<IFoodCategoryService>();
        public IFoodItemService FoodItems => ServiceProvider.GetService<IFoodItemService>();
        public IFoodItemDepletedReportService FoodItemDepletedReports => ServiceProvider.GetService<IFoodItemDepletedReportService>();
        public IFoodItemTagService FoodItemTags => ServiceProvider.GetService<IFoodItemTagService>();
        public IFoodTagService FoodTags => ServiceProvider.GetService<IFoodTagService>();
        public IPriceService Prices => ServiceProvider.GetService<IPriceService>();
        public IProviderService Providers => ServiceProvider.GetService<IProviderService>();
        public IRatingService Ratings => ServiceProvider.GetService<IRatingService>();
        public IUserFavouriteProviderService UserFavouriteProviders => ServiceProvider.GetService<IUserFavouriteProviderService>();
        
    }
}