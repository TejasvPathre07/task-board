using Microsoft.AspNetCore.Mvc;
using TaskBoard.Api.DTOs;
using TaskBoard.Api.Services.Interfaces;

namespace TaskBoard.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // ✅ GET TASKS WITH FILTER
        [HttpGet("projects/{projectId}/tasks")]
        public async Task<IActionResult> GetTasks(
            int projectId,
            [FromQuery] string? status,
            [FromQuery] string? priority,
            [FromQuery] string sortBy = "createdAt",
            [FromQuery] string sortDir = "desc",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetTasksAsync(
                projectId, status, priority, sortBy, sortDir, page, pageSize);

            return Ok(result);
        }

        // ✅ GET SINGLE TASK
        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _service.GetByIdAsync(id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        // ✅ CREATE TASK
        [HttpPost("projects/{projectId}/tasks")]
        public async Task<IActionResult> Create(int projectId, CreateTaskDto dto)
        {
            var result = await _service.CreateAsync(projectId, dto);
            return Ok(result);
        }

        // ✅ UPDATE TASK
        [HttpPut("tasks/{id}")]
        public async Task<IActionResult> Update(int id, CreateTaskDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (!result) return NotFound();

            return NoContent();
        }

        // ✅ DELETE TASK
        [HttpDelete("tasks/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}