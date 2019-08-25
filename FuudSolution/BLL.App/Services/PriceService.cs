using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using Price = BLL.App.DTO.Price;

namespace BLL.App.Services
{
    public class PriceService : BaseEntityService<BLL.App.DTO.Price, DAL.App.DTO.Price, IAppUnitOfWork>, IPriceService
    {
        public PriceService(IAppUnitOfWork uow) : base(uow, new PriceMapper())
        {
            ServiceRepository = Uow.Prices;
        }

        public async Task<List<Price>> AllForFoodItemAsync(int foodItemId)
        {
            return (await Uow.Prices.AllForFoodItemAsync(foodItemId)).Select(PriceMapper.MapFromDAL).ToList();
        }
    }
}