using BlazorBoard_Api.DataAccess;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace BlazorBoard_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly BlazorBoardDB _db;

        public UserController(BlazorBoardDB db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            var users = _db.Users;

            if (users.Count() > 0)
                return Ok(users);

            return NotFound();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult Get(int id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);

            if (user is not null)
                return Ok(user);

            return NotFound();
        }

        [HttpGet]
        [Route("GetByName/{name}")]
        public IActionResult Get(string name)
        {
            var user = _db.Users.FirstOrDefault(x => x.Name == name);
            if (user is null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(User user)
        {
            try
            {
                _db.Delete(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Success");
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(User user)
        {
            try
            {
                _db.Insert(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Success");
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(User user)
        {
            try
            {
                _db.Update(user);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok("Success");
        }
    }
}
