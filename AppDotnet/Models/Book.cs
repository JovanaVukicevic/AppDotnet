namespace AppDotnet.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Genre Genre { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; }
    }
}
