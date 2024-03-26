using Common.Model;
using Domain.Model;
using Infrastructure.DataAccess.Context;
using Infrastructure.DataAccess.SeedWork;

namespace Infrastructure.DataAccess.DataBaseConfig.Repositories
{
    public class GoalRepository : Repository<Goal>, IGoalRepository
    {
        private readonly IDatabaseContext _dataBase;

        public GoalRepository(IDatabaseContext dataBase) : base(dataBase)
        {
            _dataBase = dataBase;
        }

        public Goal GetGoalById(int GoalId)
        {
            return _dataBase.DtGoals.Find(GoalId);
        }

        public Goal GetGoalByName(int GoalId, string Name)
        {
            Goal _goal = new Goal();
            if (GoalId == 0)
                _goal = _dataBase.DtGoals.Where(f => f.Name.Trim().ToLower() == Name.Trim().ToLower()).FirstOrDefault();
            else
            {
                //Verificar que no exista el nombre en algun otra meta
                _goal = _dataBase.DtGoals.Where(f => f.Name.Trim().ToLower() == Name.Trim().ToLower()).FirstOrDefault();
                if (_goal != null && _goal.GoalId != GoalId)
                    return null;
                _goal = _dataBase.DtGoals.Find(GoalId);
            }
            return _goal;
        }

        public GTask GetTaskById(int TaskId)
        {
            return _dataBase.DtTasks.Find(TaskId);
        }

        public List<GTask> GetTasksByGoalId(int GoalId)
        {
            return _dataBase.DtTasks.Where(f=>f.Goal.GoalId == GoalId).ToList();
        }

        public GTask GetTaskByName(int TaskId, string Name)
        {            
            GTask _task = new GTask();
            if (TaskId == 0)
                _task = _dataBase.DtTasks.Where(f => f.Name.Trim().ToLower() == Name.Trim().ToLower()).FirstOrDefault();
            else
            {
                //Verificar que no exista el nombre en algun otra meta
                _task = _dataBase.DtTasks.Where(f => f.Name.Trim().ToLower() == Name.Trim().ToLower()).FirstOrDefault();
                if (_task != null && _task.TaskId != TaskId)
                    return null;
                _task = _dataBase.DtTasks.Find(TaskId);
            }
            return _task;
        }

        public GState GetStatus(int StatusId)
        {
            return _dataBase.DtStatus.Find(StatusId);
        }

        public void InsertTask(GTask data)
        {
            _dataBase.DtTasks.Add(data);
        }

        public void RemoveTask(GTask data)
        {
            _dataBase.DtTasks.Remove(data);
        }

        public void RemoveTaskRange(int GoalId)
        {
            _dataBase.DtTasks.RemoveRange(_dataBase.DtTasks.Where(f=>f.Goal.GoalId == GoalId));
        }
    }
}