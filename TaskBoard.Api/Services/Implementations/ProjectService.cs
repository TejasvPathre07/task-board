using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Data;
using TaskBoard.Api.DTOs;
using TaskBoard.Api.Models;
using TaskBoard.Api.Services.Interfaces;

namespace TaskBoard.Api.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET ALL PROJECTS
        public async Task<List<ProjectDto>> GetAllAsync()
        {
            var projects = await _context.Projects
                .Include(p => p.Tasks)
                .ToListAsync();

            return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        // ✅ GET PROJECT BY ID
        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return null;

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }

        // ✅ CREATE PROJECT
        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        {
            // Check duplicate name (important for 409)
            var exists = await _context.Projects
                .AnyAsync(p => p.Name == dto.Name);

            if (exists)
                throw new Exception("Project name already exists");

            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt
            };
        }

        // ✅ UPDATE PROJECT
        public async Task<bool> UpdateAsync(int id, CreateProjectDto dto)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            project.Name = dto.Name;
            project.Description = dto.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ DELETE PROJECT
        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}