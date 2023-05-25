using Microsoft.AspNetCore.Mvc;
using WebApplicationFER.DataBase;

namespace WebApplicationFER.Controllers
{
    public class EquipmentTypeController : Controller
    {
        private NewDbPracContext _dbContext;
        public EquipmentTypeController(NewDbPracContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Json(_dbContext.EquipmentTypes.FirstOrDefault(equipmentType => equipmentType.Id == id));
        }

        [HttpPost]
        public IActionResult Create(EquipmentType equipmentType)
        {
            if (_dbContext.EquipmentTypes.Any(innerType => innerType.Id == equipmentType.Id) == false)
            {
                _dbContext.EquipmentTypes.Add(equipmentType);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult Edit(EquipmentType equipmentType)
        {
            if (_dbContext.EquipmentTypes.Any(innerType => innerType.Id == equipmentType .Id))
            {
                _dbContext.EquipmentTypes.Update(equipmentType);

                _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var equipmentToDelete = _dbContext.EquipmentTypes.FirstOrDefault(innerType => innerType.Id == id);

            if (equipmentToDelete == default)
                return BadRequest();

            _dbContext.EquipmentTypes.Remove(equipmentToDelete);

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
