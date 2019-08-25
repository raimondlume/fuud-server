using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
{
    public class FoodCategory
    {
        public int Id { get; set; }
        
        // TODO: add estonian and english options
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string FoodCategoryValue { get; set; }
    }
}