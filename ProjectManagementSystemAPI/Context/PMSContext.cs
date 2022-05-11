using Microsoft.EntityFrameworkCore;
using ProjectManagementSystemAPI.Models;

namespace ProjectManagementSystemAPI.Context
{
    public class PMSContext : DbContext
    {
        public PMSContext(DbContextOptions<PMSContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Activity> Activities { get; set; }

    }
}
