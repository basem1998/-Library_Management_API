using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryManagement.Models
{
    public class BorrowingRecord
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int BookID { get; set; }

        [ForeignKey("BookID")]
        public Book Book { get; set; }

        [Required]
        public int PatronID { get; set; }

        [ForeignKey("PatronID")]
        public Patron Patron { get; set; }

        [Required]
        public DateTime BorrowingDate { get; set; } = DateTime.UtcNow;

        public DateTime? ReturnDate { get; set; }
    }
}
