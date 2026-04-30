using TaskBoard.Api.Models;

namespace TaskBoard.Api.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime? DueDate { get; set; }
    }
}