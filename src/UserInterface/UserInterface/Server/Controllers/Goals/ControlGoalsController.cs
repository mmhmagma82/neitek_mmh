using Common.Model;
using Microsoft.AspNetCore.Mvc;
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
        [Route("allgoals")]
        public IActionResult GetGoals([FromBody] GoalVM goal)
        {
            try
            {
                List<GoalVM> _getGoals = _apiClient.Post<List<GoalVM>, GoalVM>("tek", "goal/goals", goal).Data;
                return Ok(_getGoals);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }

        [HttpPost]
        [Route("savegoal")]
        public IActionResult SaveNewGoal([FromBody] GoalVM goal)
        {
            try
            {
                _apiClient.Post<List<GoalVM>, GoalVM>("tek", "goal/savegoal", goal);
                return Ok();
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
                _apiClient.Post<List<TaskVM>, TaskVM>("tek", "task/savetask", task);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }
    }
}