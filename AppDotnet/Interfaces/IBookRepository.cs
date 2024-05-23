using AppDotnet.Models;

namespace AppDotnet.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        Book GetBook(string name);
        Book GetBook(int id);

        ICollection<Borrowing> GetBorrowingOfABook(int bookId);
        bool BookExistsId(int id);
        bool CreateBook(Book b);
        bool Save();
        bool UpdateBook(Book b);
        bool BookExists(string name);
        bool DeleteBook(Book b);

    }
}
