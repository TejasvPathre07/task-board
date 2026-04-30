using TaskBoard.Api.DTOs;

namespace TaskBoard.Api.Services.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentDto>> GetByTaskIdAsync(int taskId);
        Task<CommentDto> CreateAsync(int taskId, CreateCommentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}