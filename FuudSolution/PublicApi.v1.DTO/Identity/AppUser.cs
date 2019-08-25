using System.ComponentModel.DataAnnotations;

namespace PublicApi.v1.DTO.Identity
{
    public class AppUser
    {
        public int Id { get; set; }
        
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string FirstName { get; set; }
        
        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string LastName { get; set; }
    }
}