using Application.Service.SeedWork;
using Common.Model;
using System.Collections.Generic;

namespace Application.Service.Queries
{
    public sealed class TaskList : IQuery<List<TaskVM>>
    {
        public TaskVM TaskData { get; set; }
        public TaskList(TaskVM taskdata)
        {
            TaskData = taskdata;
        }
    }
}