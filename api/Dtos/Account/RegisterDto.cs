using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? Password { get; set; }
    }
}