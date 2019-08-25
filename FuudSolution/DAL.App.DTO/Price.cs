using System.ComponentModel.DataAnnotations;

namespace DAL.App.DTO
{
    public class Price
    {
        public int Id { get; set; }

        [Required]
        public decimal PriceValue { get; set; }
        
        [MaxLength(64)]
        [MinLength(1)]
        public string ModifierName { get; set; }

        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}