using TaskBoard.Api.DTOs;

namespace TaskBoard.Api.Services.Interfaces
{
    public interface ITaskService
    {
        Task<object> GetTasksAsync(
            int projectId,
            string? status,
            string? priority,
            string sortBy,
            string sortDir,
            int page,
            int pageSize
        );

        Task<TaskDto?> GetByIdAsync(int id);
        Task<TaskDto> CreateAsync(int projectId, CreateTaskDto dto);
        Task<bool> UpdateAsync(int id, CreateTaskDto dto);
        Task<bool> DeleteAsync(int id);
    }
}