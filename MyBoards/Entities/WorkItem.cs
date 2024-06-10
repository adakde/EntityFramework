﻿namespace MyBoards.Entities
{
    public class WorkItem
    {
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



    }
}
