using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto
{
    public class StockDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "The maximum length is 20 characters")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "There should be atleast 5 characters...")]
        [MaxLength(50, ErrorMessage = "The maximum length is 50 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(5000, 100000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "There should be atleast 5 characters...")]
        [MaxLength(50, ErrorMessage = "The maximum length is 50 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1000, 1000000000)]
        public long MarketCap { get; set; }
        public List<CommentDto>? Comments { get; set; }
    }
}
