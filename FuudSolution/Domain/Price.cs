using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Price : DomainEntity
    {
        [Required]
        public decimal PriceValue { get; set; }
        
        [MaxLength(64)]
        [MinLength(1)]
        public string ModifierName { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}