using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Interfaces;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]
    public class TaskController : ControllerBase
    {
        // GET: api/<TaskController>
        private readonly ITask _taskBo;
        private readonly IUser _userBo;
        public TaskController(ITask taskBo, IUser userBo)
        {
            _taskBo = taskBo;
            _userBo= userBo;
        }

        [HttpGet]
        public IActionResult Get()
        { 
            var data = _taskBo.GetAllTasks();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var data = _taskBo.GetTaskById(id);
            if (data != null) return Ok(data); 
            else return NotFound($"No User fount with the id={id}");
        }

        [HttpPost]
        public ActionResult Post([FromBody] Task task)
        {
            var data = _taskBo.Create(task);
            if (data == null)
            {
                return BadRequest("Something went wrong, try again");
            }
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + task.Id, task);
        }

        [HttpPut]
        public ActionResult Put([FromBody] Task task)
        {
            var data = _taskBo.Update(task);
            if (data != null) return Ok(data);
            else return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetTasksByUserID(int userId)
        {
            if (_userBo.IsUserExists(userId))
            {
                var data = _taskBo.GetTasksByUserID(userId);
                return Ok(data);
            }
            else return NotFound($"User with Id: {userId} dosen't exists ");
        }
    }
}
