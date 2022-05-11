using System;

namespace ProjectManagementSystemAPI.Dtos
{
    public class ProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
