namespace ProjectManagementSystemAPI.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
