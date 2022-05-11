using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystemAPI.Context;
using ProjectManagementSystemAPI.Dtos;
using ProjectManagementSystemAPI.Models;

namespace ProjectManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly PMSContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(PMSContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        // GET: api/completed/user/5
        [HttpGet("completed/user/{id}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetCompletedProjects(int id)
        {
            return await _context.Projects.Where(p => p.Status == "completed" && p.Users.Any(u => u.Id == id)).ToListAsync();
        }

        // GET: api/ongoing/user/5
        [HttpGet("ongoing/user/{id}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetOngoingProjects(int id)
        {
            return await _context.Projects.Where(p => p.Status == "ongoing" && p.Users.Any(u => u.Id == id)).ToListAsync();
        }


        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            var project = await _context.Projects.Include(p => p.Users).ThenInclude(u => u.Role).Include(p => p.Activities).FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpPost("user-project")]
        public async Task<IActionResult> AddUserToProject(UserProjectDto userProject)
        {
            var userId = userProject.UserId;
            var projectId = userProject.ProjectId;

            var project = await _context.Projects.Include(p=>p.Users).FirstOrDefaultAsync(p => p.Id == projectId);
            var user = await _context.Users.FindAsync(userId);

            if (project == null || user == null)
            {
                return NotFound();
            }

            project.Users.Add(user);
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Status=200,
                Message = $"Added {user.Name} to project {project.Name}"
            });
        }

        [HttpPut("remove-user")]
        public async Task<IActionResult> RemoveUserFromProject(UserProjectDto userProject)
        {
            var userId = userProject.UserId;
            var projectId = userProject.ProjectId;

            var project = await _context.Projects.Include(p => p.Users).FirstOrDefaultAsync(p => p.Id == projectId);
            var user = await _context.Users.FindAsync(userId);

            if (project == null || user == null)
            {
                return NotFound();
            }

            project.Users.Remove(user);
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Status = 200,
                Message = $"Removed {user.Name} from project {project.Name}"
            });
        }

        [HttpPut("complete-project/{id}")]
        public async Task<IActionResult> ChangeProjectStatusToCompleted(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            
            if (project == null)
            {
                return NotFound();
            }
            
            project.Status = "completed";
            return Ok();
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<ActionResult<Project>> PostProject(int id, ProjectDto projectDto)
        {
            if (projectDto == null)
            {
                return BadRequest();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var project = _mapper.Map<Project>(projectDto);
            var users = new List<User>()
            {
                user
            };
            project.Users = users;
            project.Status = "ongoing";
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Status=200,
                Message=$"{project.Name} created!"
            });
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
