using System;
using System.Collections.Generic;

namespace ProjectManagementSystemAPI.Models
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<ActivityDto> Activities { get; set; }

        public int UserProjectId { get; set; }
        public List<UserProjectDto> UserProject { get; set; }
    }
}
