using System;

namespace ProjectManagementSystemAPI.Dtos
{
    public class SignupDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
