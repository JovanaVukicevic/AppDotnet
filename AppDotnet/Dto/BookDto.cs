using AppDotnet.Models;

namespace AppDotnet.Dto
{
    public class BookDto
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Author { get; set; }
        public string genreName { get; set; }
    }
}
