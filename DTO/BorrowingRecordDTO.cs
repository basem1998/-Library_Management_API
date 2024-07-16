namespace LibraryManagement.DTO
{
    public class BorrowingRecordDTO
    {
        //public int ID { get; set; }
        public int BookID { get; set; }
        public int PatronID { get; set; }
        public DateTime BorrowingDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
