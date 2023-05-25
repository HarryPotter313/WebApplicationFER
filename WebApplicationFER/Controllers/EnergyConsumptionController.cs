using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    public class EnergyConsumptionController : Controller
    {
        private NewDbPracContext _dbContext;
        public EnergyConsumptionController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(string code)
        {
            return Json(_dbContext.EnergyConsumptions.FirstOrDefault(energyConsumption => energyConsumption.EquipmentCode == code));
        }

        [HttpPost]
        public IActionResult Create(EnergyConsumption energyConsumption)
        {
            if (_dbContext.EnergyConsumptions.Any(innerEnergyConsumption => innerEnergyConsumption.EquipmentCode == energyConsumption.EquipmentCode) == false)
            {
                _dbContext.EnergyConsumptions.Add(energyConsumption);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Edit(EnergyConsumption energyConsumption)
        {
            if (_dbContext.EnergyConsumptions.Any(innerConsumption => innerConsumption.EquipmentCode == energyConsumption.EquipmentCode))
            {
                _dbContext.EnergyConsumptions.Update(energyConsumption);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(string code)
        {
            var consumptionToDelete = _dbContext.EnergyConsumptions.FirstOrDefault(innerConsumption => innerConsumption.EquipmentCode == code);

            if (consumptionToDelete == default)
                return BadRequest();

            _dbContext.EnergyConsumptions.Remove(consumptionToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
