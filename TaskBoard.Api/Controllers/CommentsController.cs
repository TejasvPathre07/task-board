using Microsoft.AspNetCore.Mvc;
using TaskBoard.Api.DTOs;
using TaskBoard.Api.Services.Interfaces;

namespace TaskBoard.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        // ✅ GET COMMENTS
        [HttpGet("tasks/{taskId}/comments")]
        public async Task<IActionResult> Get(int taskId)
        {
            var result = await _service.GetByTaskIdAsync(taskId);
            return Ok(result);
        }

        // ✅ ADD COMMENT
        [HttpPost("tasks/{taskId}/comments")]
        public async Task<IActionResult> Create(int taskId, CreateCommentDto dto)
        {
            var result = await _service.CreateAsync(taskId, dto);
            return Ok(result);
        }

        // ✅ DELETE COMMENT
        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}