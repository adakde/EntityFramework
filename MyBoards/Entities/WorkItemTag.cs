namespace MyBoards.Entities
{
    public class WorkItemTag
    {
        
        public WorkItem WorkItem { get; set; }
        public int WorkIteamId { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
        public DateTime PublicationDate { get; set; }
        public object WorkItemId { get; internal set; }
    }
}
