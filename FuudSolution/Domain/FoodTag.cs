using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class FoodTag : DomainEntity
    {
        // TODO: estonian and english values
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string FoodTagValue { get; set; }
    }
}