using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class FoodCategory : DomainEntity
    {
        // TODO: add estonian and english options
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string FoodCategoryValue { get; set; }
    }
}