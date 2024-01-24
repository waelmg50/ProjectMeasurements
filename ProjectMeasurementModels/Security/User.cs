using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ProjectsMeasurements.Models.BaseModels;

namespace ProjectsMeasurements.Models.Security
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [PasswordPropertyText]
        [MaxLength(500)]
        public string UserPassword { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string UserEmail { get; set; } = string.Empty;
        public bool IsEmailVerified { get; set; }

    }
}
