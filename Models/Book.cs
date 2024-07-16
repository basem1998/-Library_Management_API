using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        [Range(1000, 9999, ErrorMessage = "Publication year must be between 1000 and the current year")]
        public int PublicationYear { get; set; }

        [Required]
        [StringLength(13)]
        [RegularExpression(@"^[0-9]{3}-[0-9]{10}$", ErrorMessage = "ISBN must follow the pattern XXX-XXXXXXXXXX")]
        public string ISBN { get; set; }
    }
}

