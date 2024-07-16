using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Patron
    {
        [Key] 
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string ContactInformation { get; set; }
    }
}
