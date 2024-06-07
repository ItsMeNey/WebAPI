using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto
{
    public class CommentDto
    {
        [Required]
        [Range(1, 20)]
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "There should be atleast 5 characters...")]
        [MaxLength(50, ErrorMessage = "The maximum length is 50 characters")]

        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength(150, ErrorMessage = "The maximum length is 150 characters")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        //public string AppUserName { get; set; } = string.Empty;
        //public string AppUserId { get; set; } = string.Empty;
        [Required]
        [Range(0, 100)]
        public int StockId { get; set; }

    }
}
