using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    public class UsersController : Controller
    {
        private NewDbPracContext _dbContext;
        public UsersController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.Users.FirstOrDefault(user => user.Id == id));
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (_dbContext.Users.Any(innerUser => innerUser.Id == user.Id) == false)
            {
                _dbContext.Users.Add(user);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Edit(User user)
        {
            if (_dbContext.Users.Any(innerUser => innerUser.Id == user.Id))
            {
                _dbContext.Users.Update(user);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userToDelete = _dbContext.Users.FirstOrDefault(innerUser => innerUser.Id == id);

            if (userToDelete == default)
                return BadRequest();

            _dbContext.Users.Remove(userToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
