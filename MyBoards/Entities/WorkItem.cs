using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBoards.Entities
{
    public class Epic : WorkItem
    {
        //Epic
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class Issue : WorkItem
    {
        public decimal Efford { get; set; }
    }
    public class Task : WorkItem
    {
        public string Activity { get; set; }
        public decimal RemaningWork { get; set; }

    }
    public abstract class WorkItem
    {
        public int Id { get; set; }
        public string state {  get; set; }
        public string area { get; set; }
        public string IterationPath { get; set; }

        public int Priority { get; set; }
        public string Type { get; set; }


        public List<Comment> Comments { get; set; } = new List<Comment>();
        public User Author { get; set; }
        public Guid AuthorId { get; set; }
        public List<Tag> Tags { get; set; }
        public WorkItemState State { get; set; }
        public int StateId { get; set; }

    }
}
