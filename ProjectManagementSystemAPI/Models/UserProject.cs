namespace ProjectManagementSystemAPI.Models
{
    public class UserProjectDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }

        public int ProjectId { get; set; }
        public ProjectDto Project { get; set; }
    }
}
