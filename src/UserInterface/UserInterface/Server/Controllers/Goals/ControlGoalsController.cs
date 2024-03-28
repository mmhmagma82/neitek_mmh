using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserInterface.Server.Service;

namespace UserInterface.Server.Controllers.Goals
{
    [Route("admingoals")]
    [ApiController]
    public class ControlGoalsController : Controller
    {
        private ApiClient _apiClient;

        public ControlGoalsController()
        {
            _apiClient = new ApiClient();
        }

        [HttpPost]
        [Route("listgoals")]
        public List<GoalVM> GetGoals([FromBody] GoalVM goal)
        {
            try
            {
                List<GoalVM> _getGoals = _apiClient.Post<List<GoalVM>, GoalVM>("tek", "goal/goals", goal).Data;
                return _getGoals;
            }
            catch
            {
                return new List<GoalVM>();
            }
        }

        [HttpPost]
        [Route("savegoal")]
        public IActionResult SaveNewGoal([FromBody] GoalVM goal)
        {
            try
            {
                var result = _apiClient.Post<string, GoalVM>("tek", "goal/savegoal", goal);
                if (result.Code == ResponseCode.Success)
                    return Ok();
                else
                    return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }

        [HttpPost]
        [Route("alltasks")]
        public IActionResult GetTasks([FromBody] TaskVM task)
        {
            try
            {
                List<TaskVM> _getGoals = _apiClient.Post<List<TaskVM>, TaskVM>("tek", "task/tasks", task).Data;
                return Ok(_getGoals);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }

        [HttpPost]
        [Route("savetask")]
        public IActionResult SaveNewTask([FromBody] TaskVM task)
        {
            try
            {
                var result = _apiClient.Post<string, TaskVM>("tek", "task/savetask", task);
                if (result.Code == ResponseCode.Success)
                    return Ok();
                else
                    return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }
    }
}