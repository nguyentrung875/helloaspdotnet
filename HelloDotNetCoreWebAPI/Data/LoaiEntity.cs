using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloDotNetCoreWebAPI.Data
{
    [Table("loai")]
    public class LoaiEntity
    {
        [Key] public int id { get; set; }
        [Required][MaxLength(50)] public string name { get; set; }

        public virtual ICollection<HangHoaEntity> listHangHoa { get; set; }
    }
}
