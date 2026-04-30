using TaskBoard.Api.DTOs;

namespace TaskBoard.Api.Services.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<ProjectDto> CreateAsync(CreateProjectDto dto);
        Task<bool> UpdateAsync(int id, CreateProjectDto dto);
        Task<bool> DeleteAsync(int id);
    }
}