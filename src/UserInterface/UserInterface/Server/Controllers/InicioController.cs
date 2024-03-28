using Common.Model;
using Microsoft.AspNetCore.Mvc;
using UserInterface.Server.Service;

namespace UserInterface.Server.Controllers
{
    [Route("neitek")]
    public class InicioController : Controller
    {
        private ApiClient _apiClient;
        public InicioController()
        {
            _apiClient = new ApiClient();
        }

        [HttpGet]
        [Route("allgoals")]
        public IActionResult GetAllGoals()
        {
            try
            {
                GoalVM _newGoal = new GoalVM() { Name = "Test" };
                var _getGoals = _apiClient.Post<List<GoalVM>, GoalVM>("tek", "goal/goals", _newGoal);

                //Response<LoginVM> _data = _apiClient.Post<LoginVM, LoginVM>("wil", "user/signin", userData);

                return Ok(_getGoals);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }
    }
}
