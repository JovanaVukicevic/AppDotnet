using AppDotnet.Models;

namespace AppDotnet.Interfaces
{
    public interface IBorrowingRepository
    {
        ICollection<Borrowing> GetBorrowings();
        ICollection<Borrowing> GetBorrowingsOfAMember(int memberId);
        bool CreateBorrowing(Borrowing borrowing);
        Borrowing GetBorrowing(int borrowId);
        bool Save();
        bool BorrowExists(int memberId);
        bool BorrowingIdExists(int id);
        bool UpdateBorrowing(Borrowing b);
        bool DeleteBorrowing(ICollection<Borrowing> b);

        bool DeleteBorrowing(Borrowing b);
    }
}
