using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO
{
    public class FoodTag
    {
        public int Id { get; set; }

        // TODO: estonian and english values
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string FoodTagValue { get; set; }
    }
}