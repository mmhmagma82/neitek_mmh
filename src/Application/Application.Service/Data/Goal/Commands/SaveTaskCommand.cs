using Application.Service.SeedWork;
using Common.Model;

namespace Application.Service.Commands
{
    public sealed class SaveTaskCommand : ICommand
    {
        public TaskVM TaskData { get; }

        public SaveTaskCommand(TaskVM taskData)
        {
            TaskData = taskData;
        }
    }
}