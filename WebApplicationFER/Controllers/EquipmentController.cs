using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    public class EquipmentController : Controller
    {
        private NewDbPracContext _dbContext;
        public EquipmentController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.Equipment.FirstOrDefault(equipment => equipment.Id == id));
        }

        [HttpPost]
        public IActionResult Create(Equipment equipment)
        {
            if (_dbContext.Equipment.Any(innerEquipment => innerEquipment.Id == equipment.Id) == false)
            {
                _dbContext.Equipment.Add(equipment);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Edit(Equipment equipment)
        {
            if (_dbContext.Equipment.Any(innerEquipment => innerEquipment.Id == equipment.Id))
            {
                _dbContext.Equipment.Update(equipment);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var equipmentToDelete = _dbContext.Equipment.FirstOrDefault(innerEquipment => innerEquipment.Id == id);

            if (equipmentToDelete == default)
                return BadRequest();

            _dbContext.Equipment.Remove(equipmentToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
