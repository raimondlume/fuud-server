using PublicApi.v1.DTO.Identity;

namespace PublicApi.v1.DTO
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