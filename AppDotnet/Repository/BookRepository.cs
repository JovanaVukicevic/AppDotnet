using AppDotnet.Data;
using AppDotnet.Interfaces;
using AppDotnet.Models;

namespace AppDotnet.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
                
        }
        public bool BookExists(string name)
        {
            return _context.Books.Any(b => b.Title == name);
        }

        public bool BookExistsId(int id)
        {
            return _context.Books.Any(b => b.Id == id);
        }

        public bool CreateBook(Book b)
        {
            _context.Add(b);
            return Save();
        }

        public bool DeleteBook(Book b)
        {
            _context.Remove(b);
            return Save();
        }

        public Book GetBook(string name)
        {
            return _context.Books.Where(b => b.Title == name).FirstOrDefault();
        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(b => b.Id == id).FirstOrDefault();
        }

        public ICollection<Book> GetBooks()
        {
            return _context.Books.OrderBy(b => b.Id).ToList();
        }

        public ICollection<Borrowing> GetBorrowingOfABook(int bookId)
        {
            return _context.Borrowings.Where(b => b.BookId == bookId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBook(Book b)
        {
            _context.Update(b);
            return Save();
        }
    }
}
