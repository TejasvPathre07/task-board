using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Data;

namespace TaskBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var totalProjects = await _context.Projects.CountAsync();

            var tasks = await _context.Tasks.ToListAsync();

            var tasksByStatus = tasks
                .GroupBy(t => t.Status)
                .ToDictionary(g => g.Key.ToString(), g => g.Count());

            var overdueCount = tasks
                .Count(t => t.DueDate < DateTime.UtcNow && t.Status != Models.Status.Done);

            var dueSoon = tasks
                .Where(t => t.DueDate >= DateTime.UtcNow &&
                            t.DueDate <= DateTime.UtcNow.AddDays(7))
                .Count();

            return Ok(new
            {
                totalProjects,
                totalTasks = tasks.Count,
                tasksByStatus,
                overdueCount,
                dueSoon
            });
        }
    }
}