using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Api.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation
        public Project Project { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}