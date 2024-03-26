using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Model
{
    public class TaskVM : ResponseVM
    {
        public int Id { get; set; }
        [Required]
        [StringLength(80, ErrorMessage = "Solo se permiten 80 carácteres."), MaxLength(80)]
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsImportant { get; set; }
        public int GoalId { get; set; }
        public string GoalName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}