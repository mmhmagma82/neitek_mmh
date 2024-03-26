using Domain.Model.SeedWork;
using System.Collections.Generic;

namespace Domain.Model
{
    public interface IGoalRepository : IRepository<Goal>
    {
        Goal GetGoalById(int GoalId);
        Goal GetGoalByName(int GoalId, string Name);
        GTask GetTaskById(int TaskId);
        List<GTask> GetTasksByGoalId(int GoalId);
        GTask GetTaskByName(int TaskId, string Name);
        GState GetStatus(int StatusId);
        void InsertTask(GTask data);
        void RemoveTask(GTask data);
        void RemoveTaskRange(int GoalId);
    }
}