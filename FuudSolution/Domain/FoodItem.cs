using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class FoodItem : DomainEntity
    {
        [MaxLength(100)]
        [MinLength(1)]
        [Required]
        public string NameEst { get; set; }

        [MaxLength(100)]
        [MinLength(1)]
        public string NameEng { get; set; }

        [Required]
        public DateTime DateStart { get; set; }
        
        public DateTime? DateEnd { get; set; }

        public int? FoodCategoryId { get; set; }
        public FoodCategory FoodCategory { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public ICollection<Price> Prices { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<FoodItemDepletedReport> DepletedReports { get; set; }
        public ICollection<FoodItemTag> FoodItemTags { get; set; }
    }
}