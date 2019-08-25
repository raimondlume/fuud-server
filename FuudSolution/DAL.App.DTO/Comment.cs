using System;
using System.ComponentModel.DataAnnotations;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Comment
    {
        public int Id { get; set; }
        
        [MaxLength(140)]
        [MinLength(1)]
        [Required]
        public string CommentValue { get; set; }

        [Timestamp]
        public DateTime Timestamp { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}