using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatronsController : ControllerBase
    {
        private readonly LibraryDb _context;

        public PatronsController(LibraryDb context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var patrons = _context.Patrons.AsNoTracking().ToList();
            return Ok(patrons);
        }

        [HttpGet("{id:int}", Name = "GetPatron")]
        public IActionResult Get(int id)
        {
            var patron = _context.Patrons.AsNoTracking().FirstOrDefault(x => x.ID == id);
            if (patron == null)
            {
                return NotFound(new { Message = $"Patron with ID {id} was not found." });
            }
            return Ok(patron);
        }

        [HttpPost]
        public IActionResult Create(Patron patron)
        {
            if (ModelState.IsValid)
            {
                _context.Patrons.Add(patron);
                _context.SaveChanges();

                string url = Url.Link("GetPatron", new { id = patron.ID });
                return Created(url, patron);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Patron patron)
        {
            if (id != patron.ID)
            {
                return BadRequest(new { Message = "Patron ID mismatch." });
            }

            if (ModelState.IsValid)
            {
                var existingPatron = _context.Patrons.AsNoTracking().FirstOrDefault(p => p.ID == patron.ID);
                if (existingPatron == null)
                {
                    return NotFound(new { Message = $"Patron with ID {patron.ID} was not found." });
                }

                _context.Entry(patron).State = EntityState.Modified;
                try
                {
                    _context.SaveChanges();
                    return Ok(patron);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred while updating the patron.", Details = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var patron = _context.Patrons.FirstOrDefault(p => p.ID == id);
            if (patron == null)
            {
                return NotFound(new { Message = $"Patron with ID {id} was not found." });
            }

            _context.Patrons.Remove(patron);
            _context.SaveChanges();
            return Ok(new { Message = $"Patron with ID {id} was deleted." });
        }
    }
}
