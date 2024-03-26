using Common.Model;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Web.Server.Service;

namespace UserInterface.Web.Server.Controllers
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
                goal.Operation = (int)OperationsList.New;
                List<GoalVM> _getGoals = _apiClient.Post<List<GoalVM>, GoalVM>("tek", "goal/savegoal", goal).Data;
                return Ok(_getGoals);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }
    }
}