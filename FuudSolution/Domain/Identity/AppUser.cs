using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<int>
    {
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