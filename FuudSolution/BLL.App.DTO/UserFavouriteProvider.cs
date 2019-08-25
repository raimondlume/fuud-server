using BLL.App.DTO.Identity;

namespace BLL.App.DTO
{
    public class UserFavouriteProvider
    {
        public int Id { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}