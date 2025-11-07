using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : ControllerBase
    {
        private static List<Master> masters = new()
        {
            new Master { Id = 1, Name = "Jackson", Category = Category.Electrical, Ranking = 6.7 },
            new Master { Id = 2, Name = "Mickey", Category = Category.Flooring, Ranking = 8.9 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Master>> GetAll()
        {
            return Ok(masters);
        }

        [HttpGet("{id}")]
        public ActionResult<Master> GetById(int id)
        {
            var master = masters.FirstOrDefault(t => t.Id == id);

            if (master == null)
                return NotFound($"Майстра з ID={id} не знайдено.");

            return Ok(master);
        }

        [HttpPost]
        public IActionResult AddMaster([FromBody] Master newMaster)
        {
            newMaster.Id = masters.Count + 1;
            
            if (!Enum.IsDefined(typeof(Category), newMaster.Category))
                return BadRequest($"Invalid Difficulty value: {newMaster.Category}");
           
            masters.Add(newMaster);

            return CreatedAtAction(nameof(GetById), new { id = newMaster.Id }, newMaster);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Master updatedMaster)
        {
            var master = masters.FirstOrDefault(t => t.Id == id);

            if (master == null)
                return NotFound($"Майстра з ID={id} не знайдено.");


            master.Name = updatedMaster.Name;
            master.Category = updatedMaster.Category;
            master.Ranking = updatedMaster.Ranking;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var master = masters.FirstOrDefault(t => t.Id == id);

            if (master == null)
                return NotFound($"Майстра з ID={id} не знайдено.");

            masters.Remove(master);

            return NoContent();
        }
    }
}