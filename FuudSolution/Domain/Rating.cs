using System;
using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain
{
    public class Rating : DomainEntity
    {
        [Required]
        [Min(-2)]
        [Max(2)]
        public int RatingValue { get; set; }
        
        [Timestamp]
        public DateTime Timestamp { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
