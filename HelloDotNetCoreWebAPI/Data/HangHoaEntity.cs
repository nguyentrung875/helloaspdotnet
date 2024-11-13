using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloDotNetCoreWebAPI.Data
{
    [Table("hang_hoa")]
    public class HangHoaEntity
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        public string description { get; set; }

        [Range(0, double.MaxValue)]
        public double price { get; set; }

        public byte discount { get; set; }

        public int? loaiId { get; set; } //? là khóa ngoại
        [ForeignKey("loai_id")]
        public LoaiEntity loai { get; set; }
    }
}
