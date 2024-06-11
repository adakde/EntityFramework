using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBoards.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string state {  get; set; }
        [Column(TypeName ="varchar(200)")]
        public string area { get; set; }
        [Column("Iteration_Path")]
        public string IterationPath { get; set; }
        public int Priority { get; set; }
        //Epic
        public DateTime? StartDate { get; set; }
        [Precision(3)]
        public DateTime? EndDate { get; set;}
        //Issue
        [Column(TypeName="decimal(5,2)")]
        public decimal Efford { get; set; }
        //Task
        [MaxLength(200)]
        public string Activity{ get; set; }
        [Precision(14,2)]
        public decimal RemaningWork { get; set; }
        
        public string Type { get; set; }



    }
}
