using System.Reflection;
using HelloDotNetCoreWebAPI.Data;
using HelloDotNetCoreWebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoaiController : ControllerBase
    {
        private readonly MyDBContext _context;

        public LoaiController(MyDBContext context) {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult getAll() {
            var listLoai = _context.Loais.ToList();
            return Ok(listLoai);
        }

        [HttpGet("{id}")]
        public IActionResult getById(int id) {
            var loai = _context.Loais.FirstOrDefault(loai => loai.id == id);

            if (loai != null)
            {
                return Ok(loai); 
            } else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult createNew(Loai request) {
            try
            {
                var loaiEntity = new LoaiEntity
                {
                    name = request.name
                };
                _context.Add(loaiEntity);
                var s = _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult updateLoaiById(int id, Loai request)
        {
            var loai = _context.Loais.FirstOrDefault(loai => loai.id == id);
            if (loai != null)
            {
                loai.name = request.name;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
