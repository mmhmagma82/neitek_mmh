using Common.Model;
using HttpClient.Service.Services;

namespace HttpClient.Service
{
    public class CosumeApi
    {
        private ApiClient _apiClient = new ApiClient();
        private List<GoalVM> _getGoals = new List<GoalVM>();

        public List<GoalVM> GetGoals()
        {
            GoalVM _goals = new GoalVM();
            try
            {
                Response<List<GoalVM>> _data = _apiClient.Post<List<GoalVM>, GoalVM>("tek", "user/signin", _goals);
                if (_data != null)
                    _getGoals = _data.Data;
            }
            catch (Exception)
            {
                _goals = new GoalVM();
            }
            return _goals;
        }

        public void SaveGoal()
        {

        }
        public void GetTasks()
        {

        }

        public void SaveTask()
        {

        }
    }
}