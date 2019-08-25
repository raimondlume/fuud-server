using me.raimondlu.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ICommentService : IBaseEntityService<BLLAppDTO.Comment>, ICommentRepository<BLLAppDTO.Comment>
    {
        
    }
}