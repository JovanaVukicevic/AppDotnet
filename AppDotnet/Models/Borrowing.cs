namespace AppDotnet.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateOnly BorrowDate { get; set; }
        public bool Returned { get; set; }
    }
}
