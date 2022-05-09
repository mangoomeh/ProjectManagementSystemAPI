using System;
using System.Collections.Generic;

namespace ProjectManagementSystemAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime CreatedTime { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int UserProjectId { get; set; }
        public List<UserProject> UserProjects { get; set; }
    }
}
