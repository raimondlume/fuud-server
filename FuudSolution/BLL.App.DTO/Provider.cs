using System.ComponentModel.DataAnnotations;

namespace BLL.App.DTO
{
    public class Provider
    {
        public int Id { get; set; }

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