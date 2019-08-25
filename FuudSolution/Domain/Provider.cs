using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Provider : DomainEntity
    {
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string Name { get; set; }
        
        [MaxLength(100)]
        [MinLength(1)]
        public string Address { get; set; }

        public decimal LocationLatitude { get; set; }

        public decimal LocationLongitude { get; set; }
    }
}