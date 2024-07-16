//using LibraryManagement.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace LibraryManagement.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BooksController : ControllerBase
//    {
//        private readonly LibraryDb _context;
//        public BooksController(LibraryDb context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult GetAll()
//        {
//            var books = _context.Books.AsNoTracking().ToList();
//            return Ok(books);
//        }
//        [HttpGet("{id:int}")]
//        public IActionResult Get(int id)
//        {

//            var book = _context.Books.FirstOrDefault(x => x.ID == id);
//            if (book == null)
//            {
//                throw new KeyNotFoundException($"Book with ID {id} was not found.");
//            }
//            return Ok(book);
//        }

//        [HttpPost]
//        public IActionResult Create(Book book)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Books.Add(book);
//                _context.SaveChanges();

//                string url = Url.Action(nameof(Get), new { id = book.ID });
//                return Created(url, book);
//            }
//            else
//            {
//                return BadRequest(ModelState);
//            }
//        }


//        [HttpPost("Edit")]
//        public IActionResult Edit(Book book)
//        {
//            if (ModelState.IsValid)
//            {
//                var existingBook = _context.Books.AsNoTracking().FirstOrDefault(b => b.ID == book.ID);

//                if (existingBook == null)
//                {
//                    return NotFound(new { Message = $"Book with ID {book.ID} was not found." });
//                }

//                _context.Entry(book).State = EntityState.Modified;

//                try
//                {
//                    _context.SaveChanges();
//                    return Ok(book);
//                }
//                catch (Exception ex)
//                {
//                    return StatusCode(500, new { Message = "An error occurred while updating the book.", Details = ex.Message });
//                }
//            }
//            else
//            {
//                return BadRequest(ModelState);
//            }
//        }
//        [HttpGet("{id:int}")]
//        public IActionResult Delete(int id)
//        {
//            if (id != null)
//            {
//               var Existbook = _context.Books.FirstOrDefault(book => book.ID == id);
//                _context.Books.Remove(Existbook);
//                _context.SaveChanges();
//                return Ok();

//            }
//            return NotFound();
//        }

//    }
//}



using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDb _context;

        public BooksController(LibraryDb context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.Books.AsNoTracking().ToList();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var book = _context.Books.AsNoTracking().FirstOrDefault(x => x.ID == id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} was not found." });
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                _context.SaveChanges();

                string url = Url.Action(nameof(Get), new { id = book.ID });
                return Created(url, book);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.ID)
            {
                return BadRequest(new { Message = "Book ID mismatch." });
            }

            if (ModelState.IsValid)
            {
                var existingBook = _context.Books.AsNoTracking().FirstOrDefault(b => b.ID == book.ID);

                if (existingBook == null)
                {
                    return NotFound(new { Message = $"Book with ID {book.ID} was not found." });
                }

                _context.Entry(book).State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
                    return Ok(book);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "An error occurred while updating the book.", Details = ex.Message });
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.ID == id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} was not found." });
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok(new { Message = $"Book with ID {id} was deleted." });
        }
    }
}
