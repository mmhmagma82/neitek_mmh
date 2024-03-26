using Application.Service.SeedWork;
using Common.Model;

namespace Application.Service.Commands
{
    public sealed class SaveGoalCommand : ICommand
    {
        public GoalVM GoalData { get; }

        public SaveGoalCommand(GoalVM goalData)
        {
            GoalData = goalData;
        }
    }
}