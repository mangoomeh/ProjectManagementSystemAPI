using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystemAPI.Context;
using ProjectManagementSystemAPI.Dtos;
using ProjectManagementSystemAPI.Helpers;
using ProjectManagementSystemAPI.Models;

namespace ProjectManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly PMSContext _context;

        public LoginController(PMSContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            
            if (user == null)
            {
                return NotFound();
            }

            if (!EncryptDecryptPassword.VerifyHashPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
