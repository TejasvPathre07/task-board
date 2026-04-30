using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Api.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Author { get; set; }

        [Required]
        [MaxLength(500)]
        public string Body { get; set; }
    }
}