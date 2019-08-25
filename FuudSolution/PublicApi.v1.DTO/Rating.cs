using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.v1.DTO.Identity;

namespace PublicApi.v1.DTO
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        public int RatingValue { get; set; }
        
        [Timestamp]
        public DateTime Timestamp { get; set; }

        public int FoodItemId { get; set; }

        public int AppUserId { get; set; }
    }
}