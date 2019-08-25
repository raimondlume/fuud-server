using System;
using System.ComponentModel.DataAnnotations;
using PublicApi.v1.DTO.Identity;

namespace PublicApi.v1.DTO
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

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

    }
}