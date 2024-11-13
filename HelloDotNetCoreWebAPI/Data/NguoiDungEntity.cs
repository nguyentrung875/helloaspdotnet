using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloDotNetCoreWebAPI.Data
{

    [Table("nguoi_dung")]
    public class NguoiDungEntity
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(50)]
        public string username { get; set; }
        [Required]
        [MaxLength(50)]
        public string password { get; set; }
        public string email { get; set; }

    }
}
