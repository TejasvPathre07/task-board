using System.ComponentModel.DataAnnotations;
using TaskBoard.Api.Models;

namespace TaskBoard.Api.DTOs
{
    public class CreateTaskDto
    {
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
    }
}