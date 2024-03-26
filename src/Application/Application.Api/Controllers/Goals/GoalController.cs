using Application.Service.Commands;
using Application.Service.Queries;
using Application.Service.SeedWork;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.Api.Controllers.Goals
{

    [Route("nei/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly Messages _messages;

        public GoalController(Messages messages)
        {
            _messages = messages;
        }

        [HttpPost]
        [Route("goals")]
        public IActionResult GetGoals([FromBody] GoalVM data)
        {
            try
            {
                return Ok(_messages.Dispatch(new GoalList(data)));
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }

        [HttpPost]
        [Route("findgoal")]
        public IActionResult GetGoal([FromBody] GoalVM data)
        {
            try
            {
                return Ok(_messages.Dispatch(new GoalList(data)));
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }

        [HttpPost]
        [Route("savegoal")]
        public IActionResult SaveUser([FromBody] GoalVM data)
        {
            try
            {
                var result = _messages.Dispatch(new SaveGoalCommand(data));
                if (result.IsSuccess)
                    return Ok((int)MessageList.Success);
                else
                    return BadRequest(result.Error);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }
    }
}