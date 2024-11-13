using System.ComponentModel.DataAnnotations;

namespace HelloDotNetCoreWebAPI.Model
{
    public class Loai
    {   
        [Required] [MaxLength(50)]public string name { get; set; }
    }
}
