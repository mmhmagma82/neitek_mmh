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
    public class TaskController : ControllerBase
    {
        private readonly Messages _messages;

        public TaskController(Messages messages)
        {
            _messages = messages;
        }

        [HttpPost]
        [Route("tasks")]
        public IActionResult GetTasks([FromBody] TaskVM data)
        {
            try
            {
                return Ok(_messages.Dispatch(new TaskList(data)));
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }

        [HttpPost]
        [Route("findtask")]
        public IActionResult GetTask([FromBody] TaskVM data)
        {
            try
            {
                return Ok(_messages.Dispatch(new TaskList(data)));
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e.ToString());
            }
        }

        [HttpPost]
        [Route("savetask")]
        public IActionResult SaveTask([FromBody] TaskVM data)
        {
            try
            {
                var result = _messages.Dispatch(new SaveTaskCommand(data));
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