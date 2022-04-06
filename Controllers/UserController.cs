using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Interfaces;
using ProjectManagement.Models;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUser _userBO;

        public UserController(IUser repo)
        {
            _userBO = repo;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = _userBO.GetAllUsers();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var data = _userBO.GetUserById(id);
            if (data != null) return Ok(data);
            else return NotFound($"No User fount with the id={id}");
        }

        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            var data = _userBO.Create(user);
            if (data == null)
            {
                return BadRequest("Something went wrong, try again");
            }
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + user.Id, user);
        }

        [HttpPut]
        public ActionResult Put([FromBody] User user)
        {
            var data = _userBO.Update(user);
            if (data != null) return Ok(data);
            else return BadRequest();
        }

    }
}
