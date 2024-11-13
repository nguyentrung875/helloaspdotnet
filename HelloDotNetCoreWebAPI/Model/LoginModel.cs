using System.ComponentModel.DataAnnotations;

namespace HelloDotNetCoreWebAPI.Model
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string username { get; set; }
        [Required]
        [MaxLength(50)]
        public string password { get; set; }
    }
}
