using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class GTask
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsImportant { get; set; }
        public virtual Goal Goal { get; set; }
        public virtual GState GState { get; set; }

        [NotMapped]
        public int goalid { get; set; }
        public int stateid { get; set; }

    }
}