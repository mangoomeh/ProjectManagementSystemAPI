using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IConfiguration _config;

        public LoginController(PMSContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private string CreateJwtToken(User user)
        {
            List<Claim> claimsList = new()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Role", user.Role.Name),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("SecretKey").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claimsList,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest();
            }

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            
            if (user == null)
            {
                return NotFound();
            }

            if (!EncryptDecryptPassword.VerifyHashPassword(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            return Ok(new
            {
                Status = 200,
                Message = "Login Success!",
                Token = CreateJwtToken(user)
            });
        }
    }
}
