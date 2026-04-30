using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Api.DTOs
{
    public class CreateProjectDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }
    }
}