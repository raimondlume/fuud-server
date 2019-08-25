using System;
using System.Threading.Tasks;
using Hangfire;

namespace Contracts.WebCrawler.Base
{
    public interface IWebCrawler
    {
        [AutomaticRetry(Attempts = 0)]
        Task UpdateFoodItems();
    }
}