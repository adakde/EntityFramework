namespace MyBoards.Entities
{
    public class Comment
    {
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate {  get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
