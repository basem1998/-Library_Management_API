using LibraryManagement.DTO;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingRecordsController : ControllerBase
    {
        private readonly LibraryDb _context;

        public BorrowingRecordsController(LibraryDb context)
        {
            _context = context;
        }

        [HttpPost("Borrow")]
        public IActionResult Borrow(BorrowingRecordDTO borrowingRecordDTO)
        {
            var book = _context.Books.FirstOrDefault(b => b.ID == borrowingRecordDTO.BookID);
            var patron = _context.Patrons.FirstOrDefault(p => p.ID == borrowingRecordDTO.PatronID);

            if (book == null || patron == null)
            {
                return NotFound(new { Message = "Book or patron not found." });
            }

            var borrowingRecordsCount = _context.BorrowingRecords
                .Count(br => br.BookID == borrowingRecordDTO.BookID && br.ReturnDate == null);

            //if (borrowingRecordsCount >= book.Quantity)
            //{
            //    return BadRequest(new { Message = "No available copies of the book." });
            //}

            var borrowingRecord = new BorrowingRecord
            {
                BookID = borrowingRecordDTO.BookID,
                PatronID = borrowingRecordDTO.PatronID,
                BorrowingDate = DateTime.Now
            };

            _context.BorrowingRecords.Add(borrowingRecord);
            _context.SaveChanges();

            return Ok(new { Message = "Book successfully borrowed." });
        }



        [HttpPut("Return")]
        public IActionResult Return(BorrowingRecordDTO borrowingRecordDTO)
        {
            var borrowingRecord = _context.BorrowingRecords
                .FirstOrDefault(br => br.BookID == borrowingRecordDTO.BookID
                                      && br.PatronID == borrowingRecordDTO.PatronID
                                      && br.ReturnDate == null);

            if (borrowingRecord == null)
            {
                return NotFound(new { Message = "Borrowing record not found." });
            }

            borrowingRecord.ReturnDate = DateTime.Now;
            _context.SaveChanges();

            return Ok(new { Message = "Book successfully returned." });
        }
    }
}
