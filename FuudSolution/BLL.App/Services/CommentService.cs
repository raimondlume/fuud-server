using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using me.raimondlu.BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App.Services
{
    public class CommentService : BaseEntityService<BLL.App.DTO.Comment, DAL.App.DTO.Comment, IAppUnitOfWork>,
        ICommentService
    {
        public CommentService(IAppUnitOfWork uow) : base(uow, new CommentMapper())
        {
            ServiceRepository = Uow.Comments;
        }

        public async Task<List<BLL.App.DTO.Comment>> AllForFoodItemAsync(int foodItemId)
        {
            return (await Uow.Comments.AllForFoodItemAsync(foodItemId)).Select(CommentMapper.MapFromDAL).ToList();
        }

        public async Task<bool> BelongsToUserAsync(int id, int userId)
        {
            return await Uow.Comments.BelongsToUserAsync(id, userId);;
        }
    }
}