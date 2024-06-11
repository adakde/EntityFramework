namespace MyBoards.Entities
{
    public class Comment
    {
        
        public int Id { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate {  get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }

    }
}
