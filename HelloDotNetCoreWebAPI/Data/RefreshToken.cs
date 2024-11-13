using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloDotNetCoreWebAPI.Data
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public Guid id { get; set; }
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public NguoiDungEntity nguoiDung { get; set; }

        public string token  { get; set; }
        public string jwtId { get; set; }
        public bool isUsed { get; set; }
        public bool isRevoked { get; set; }
        public DateTime issuedAt { get; set; }
        public DateTime expiredAt { get; set; }
    }
}
