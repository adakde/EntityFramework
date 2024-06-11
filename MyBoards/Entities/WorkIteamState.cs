namespace MyBoards.Entities
{
    public class WorkIteamState
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public WorkItem WorkItem { get; set; }
        public int WorkIteamId { get; set; }
    }
}
