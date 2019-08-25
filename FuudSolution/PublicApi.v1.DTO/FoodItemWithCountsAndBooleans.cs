using System;
using System.Collections.Generic;

namespace PublicApi.v1.DTO
{
    public class FoodItemWithCountsAndBooleans
    {
        public int Id { get; set; }

        public string NameEst { get; set; }
        
        public string NameEng { get; set; }
        
        public DateTime DateStart { get; set; }
        
        public DateTime? DateEnd { get; set; }

        public int? FoodCategoryId { get; set; }
        public FoodCategory FoodCategory { get; set; }

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        
        public ICollection<Price> Prices { get; set; }
        
        public ICollection<FoodItemTag> FoodItemTags { get; set; }
        
        public int DepletedReportCount { get; set; }
        public int RatingCount { get; set; }
        public int CommentCount { get; set; }

        public int UserRating { get; set; }
        public bool HasUserMadeDepletedReport { get; set; }
    }
}