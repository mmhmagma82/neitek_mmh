using System;

namespace Domain.Model
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public int TotalTasks { get; set; }
        public double Percentege { get; set; }
    }
}