using System.Collections.Generic;

namespace ProjectManagementSystemAPI.Models
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
