namespace ProjectManagementSystemAPI.Models
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProjectId { get; set; }
        public ProjectDto Project { get; set; }
    }
}
