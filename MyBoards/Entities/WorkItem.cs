using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBoards.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string state {  get; set; }
        public string area { get; set; }
        public string IterationPath { get; set; }

        public int Priority { get; set; }
        //Epic
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
        //Issue
        public decimal Efford { get; set; }
        //Task
        public string Activity{ get; set; }
        public decimal RemaningWork { get; set; }
        
        public string Type { get; set; }


        public List<Comment> Comments { get; set; } = new List<Comment>();
        public User Author { get; set; }
        public Guid AuthorId { get; set; }
        public List<Tag> Tags { get; set; }
        public WorkIteamState State { get; set; }
        public int StateId { get; set; }

    }
}
