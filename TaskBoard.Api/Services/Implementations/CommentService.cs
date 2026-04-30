using Microsoft.EntityFrameworkCore;
using TaskBoard.Api.Data;
using TaskBoard.Api.DTOs;
using TaskBoard.Api.Models;
using TaskBoard.Api.Services.Interfaces;

namespace TaskBoard.Api.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET COMMENTS BY TASK
        public async Task<List<CommentDto>> GetByTaskIdAsync(int taskId)
        {
            var comments = await _context.Comments
                .Where(c => c.TaskId == taskId)
                .ToListAsync();

            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                TaskId = c.TaskId,
                Author = c.Author,
                Body = c.Body,
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        // ✅ CREATE COMMENT
        public async Task<CommentDto> CreateAsync(int taskId, CreateCommentDto dto)
        {
            var comment = new Comment
            {
                TaskId = taskId,
                Author = dto.Author,
                Body = dto.Body
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return new CommentDto
            {
                Id = comment.Id,
                TaskId = comment.TaskId,
                Author = comment.Author,
                Body = comment.Body,
                CreatedAt = comment.CreatedAt
            };
        }

        // ✅ DELETE COMMENT
        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}