using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class GoalVM : ResponseVM
    {
        public GoalVM()
        {
            Name = "";
            RegisterDate = DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "Solo se permiten 80 carácteres."), MaxLength(80)]
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public int CompleteTasks { get; set; }
        public int Tasks { get; set; }
        public double Progress { get; set; }
    }
}