using Domain.Identity;

namespace Domain
{
    public class UserFavouriteProvider : DomainEntity
    {
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}