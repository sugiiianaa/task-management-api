using System.ComponentModel.DataAnnotations;

namespace TaskManagementAPI.Models.RequestModel.Auth
{
    public class RegisterRequest
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string ReTypePassword { get; set; }
        [Required]
        public required string Name { get; set; }
    }
}
