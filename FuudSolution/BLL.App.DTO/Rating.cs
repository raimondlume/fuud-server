using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;

namespace BLL.App.DTO
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        public int RatingValue { get; set; }
        
        [Timestamp]
        public DateTime Timestamp { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}