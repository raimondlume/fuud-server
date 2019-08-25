using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using me.raimondlu.Contracts.DAL.Base.Helpers;
using DAL.App.EF.Repositories;
using me.raimondlu.DAL.Base.EF;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext dbContext, IBaseRepositoryProvider repositoryProvider) : base(dbContext, repositoryProvider)
        {
        }

        public ICommentRepository Comments =>
            _repositoryProvider.GetRepository<ICommentRepository>();

        public IFoodCategoryRepository FoodCategories =>
            _repositoryProvider.GetRepository<IFoodCategoryRepository>();

        public IFoodItemDepletedReportRepository FoodItemDepletedReports =>
            _repositoryProvider.GetRepository<IFoodItemDepletedReportRepository>();

        public IFoodItemRepository FoodItems =>
            _repositoryProvider.GetRepository<IFoodItemRepository>();

        public IFoodItemTagRepository FoodItemTags =>
            _repositoryProvider.GetRepository<IFoodItemTagRepository>();

        public IFoodTagRepository FoodTags =>
            _repositoryProvider.GetRepository<IFoodTagRepository>();

        public IPriceRepository Prices =>
            _repositoryProvider.GetRepository<IPriceRepository>();

        public IProviderRepository Providers =>
            _repositoryProvider.GetRepository<IProviderRepository>();

        public IRatingRepository Ratings =>
            _repositoryProvider.GetRepository<IRatingRepository>();

        public IUserFavouriteProviderRepository UserFavouriteProviders =>
            _repositoryProvider.GetRepository<IUserFavouriteProviderRepository>();
    }
}