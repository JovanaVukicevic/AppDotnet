using AppDotnet.Data;
using AppDotnet.Interfaces;
using AppDotnet.Models;

namespace AppDotnet.Repository
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly DataContext _context;
        public BorrowingRepository(DataContext context)
        {
                _context = context;
        }
        public bool BorrowExists(int memberId)
        {
            return _context.Borrowings.Any(b => b.MemberId == memberId);
        }

        public ICollection<Borrowing> GetBorrowingsOfAMember(int memberId)
        {
            return _context.Borrowings.Where(b => b.MemberId == memberId).ToList();
        }

        public ICollection<Borrowing> GetBorrowings()
        {
            return _context.Borrowings.ToList();
        }

        public bool CreateBorrowing(Borrowing borrowing)
        {
            _context.Add(borrowing);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBorrowing(Borrowing b)
        {
            _context.Update(b);
            return Save();
        }

        public bool DeleteBorrowing(ICollection<Borrowing> b)
        {
            _context.RemoveRange(b);
            return Save();
        }

        public Borrowing GetBorrowing(int borrowId)
        {
            return _context.Borrowings.Where(b => b.Id == borrowId).FirstOrDefault();
        }

        public bool DeleteBorrowing(Borrowing b)
        {
            _context.Remove(b);
            return Save();
        }

        public bool BorrowingIdExists(int id)
        {
            return _context.Borrowings.Any(b => b.Id == id);
        }
    }
}
