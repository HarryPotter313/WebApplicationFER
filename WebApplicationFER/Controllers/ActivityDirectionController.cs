using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    public class ActivityDirectionController : Controller
    {
        private NewDbPracContext _dbContext;
        public ActivityDirectionController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.ActivityDirections.FirstOrDefault(energyConsumption => energyConsumption.Id == id));
        }

        [HttpPost]
        public IActionResult Create(ActivityDirection activityDirection)
        {
            if (_dbContext.ActivityDirections.Any(innerDirection => innerDirection.Id == activityDirection.Id) == false)
            {
                _dbContext.ActivityDirections.Add(activityDirection);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Edit(ActivityDirection activityDirection)
        {
            if (_dbContext.ActivityDirections.Any(innerDirection => innerDirection.Id == activityDirection.Id))
            {
                _dbContext.ActivityDirections.Update(activityDirection);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var directionToDelete = _dbContext.ActivityDirections.FirstOrDefault(innerDirection => innerDirection.Id == id);

            if (directionToDelete == default)
                return BadRequest();

            _dbContext.ActivityDirections.Remove(directionToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
