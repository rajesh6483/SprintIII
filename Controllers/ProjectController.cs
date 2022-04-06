using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Interfaces;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        // GET: api/<ProjectController>
        private readonly IProject _projectBO;

        public ProjectController(IProject projectBO)
        {
            _projectBO = projectBO;
        }

        /// <summary>
        /// Loads All projecxts without Tasks info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var data = _projectBO.GetAllProjects();
            return Ok(data);
        }

        /// <summary>
        /// Loads all Projects along with Tasks of Each project
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllProjectsWithTasks()
        {
            var data = _projectBO.GetAllProjectsWithTasks();
            return Ok(data);
        }

        /// <summary>
        /// Loads project along with its Taks for Given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var data = _projectBO.GetProjectById(id);
            if (data != null) return Ok(data);
            else return NotFound($"No Project found with the id={id}");
        }

        /// <summary>
        /// Creates a new Project based on Input 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Project project)
        {
            var data = _projectBO.Create(project);
            if (data == null)
            {
                return BadRequest("Something went wrong, try again");
            }
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + project.Id, project);
        }

        [HttpPut]
        public ActionResult Put([FromBody] Project project)
        {
            var data = _projectBO.Update(project);
            if (data != null) return Ok(data);
            else return BadRequest();
        }
    }
}
