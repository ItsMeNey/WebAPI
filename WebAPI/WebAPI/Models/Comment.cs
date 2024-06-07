namespace WebAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        //public string AppUserName { get; set; } = string.Empty;
        //public string AppUserId { get; set; }
        public int? StockId { get; set; }
        //Navigation
        public Stock? Stock { get; set; }


    }
}
