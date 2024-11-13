using HelloDotNetCoreWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> listHangHoa = new List<HangHoa>();

        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(listHangHoa);
        }

        [HttpPost]
        public IActionResult create(HangHoaVM hangHoaVM)
        {
            var hangHoa = new HangHoa
            {
                maHangHoa = Guid.NewGuid(),
                tenHangHoa = hangHoaVM.tenHangHoa,
                donGia = hangHoaVM.donGia
            };

            listHangHoa.Add(hangHoa);
            return Ok(new{
                Success = true,
                Data = hangHoa
            });

        }

    }
}
