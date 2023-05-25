using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    public class EnergyTypeController : Controller
    {
        private NewDbPracContext _dbContext;
        public EnergyTypeController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.EnergyTypes.FirstOrDefault(energyType => energyType.Id == id));
        }

        [HttpPost]
        public IActionResult Create(EnergyType energyType)
        {
            if (_dbContext.EnergyTypes.Any(innerEnergyType => innerEnergyType.Id == energyType.Id) == false)
            {
                _dbContext.EnergyTypes.Add(energyType);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Edit(EnergyType energyType)
        {
            if (_dbContext.EnergyTypes.Any(innerType => innerType.Id == energyType.Id))
            {
                _dbContext.EnergyTypes.Update(energyType);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var equipmentToDelete = _dbContext.EnergyTypes.FirstOrDefault(innerEnergyType => innerEnergyType.Id == id);

            if (equipmentToDelete == default)
                return BadRequest();

            _dbContext.EnergyTypes.Remove(equipmentToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
