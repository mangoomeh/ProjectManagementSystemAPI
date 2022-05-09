using System;
using System.Collections.Generic;
using ProjectManagementSystemAPI.Models;

namespace ProjectManagementSystemAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedTime { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
