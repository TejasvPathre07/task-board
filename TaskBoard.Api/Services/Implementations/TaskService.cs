using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Data;
using TaskBoard.Api.DTOs;
using TaskBoard.Api.Models;
using TaskBoard.Api.Services.Interfaces;

namespace TaskBoard.Api.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ FILTER + SORT + PAGINATION
        public async Task<object> GetTasksAsync(
            int projectId,
            string? status,
            string? priority,
            string sortBy,
            string sortDir,
            int page,
            int pageSize)
        {
            var query = _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .AsQueryable();

            // 🔹 FILTER
            if (!string.IsNullOrEmpty(status) &&
                Enum.TryParse<Status>(status, true, out var statusEnum))
            {
                query = query.Where(t => t.Status == statusEnum);
            }

            if (!string.IsNullOrEmpty(priority) &&
                Enum.TryParse<Priority>(priority, true, out var priorityEnum))
            {
                query = query.Where(t => t.Priority == priorityEnum);
            }

            // 🔹 SORT
            query = (sortBy.ToLower(), sortDir.ToLower()) switch
            {
                ("duedate", "asc") => query.OrderBy(t => t.DueDate),
                ("duedate", "desc") => query.OrderByDescending(t => t.DueDate),
                ("priority", "asc") => query.OrderBy(t => t.Priority),
                ("priority", "desc") => query.OrderByDescending(t => t.Priority),
                _ => query.OrderByDescending(t => t.CreatedAt)
            };

            // 🔹 PAGINATION
            var totalCount = await query.CountAsync();

            var tasks = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var data = tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                DueDate = t.DueDate
            });

            return new
            {
                data,
                page,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        // ✅ GET BY ID
        public async Task<TaskDto?> GetByIdAsync(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                DueDate = task.DueDate
            };
        }

        // ✅ CREATE
        public async Task<TaskDto> CreateAsync(int projectId, CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                ProjectId = projectId,
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority,
                Status = dto.Status,
                DueDate = dto.DueDate
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return new TaskDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,
                DueDate = task.DueDate
            };
        }

        // ✅ UPDATE
        public async Task<bool> UpdateAsync(int id, CreateTaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = dto.Priority;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;

            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}