using AppDotnet.Data;
using AppDotnet.Interfaces;
using AppDotnet.Models;

namespace AppDotnet.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly DataContext _context;
        public MemberRepository(DataContext context)
        {
                _context = context;
        }
       

        public ICollection<Borrowing> GetBorrowings(int memId)
        {
            return _context.Borrowings.Where(b => b.MemberId == memId).ToList();
        }

        public Member GetMember(int id)
        {
            return _context.Members.Where(m => m.Id == id).FirstOrDefault();
        }

        public ICollection<Member> GetMembers()
        {
            return _context.Members.OrderBy(m => m.Id).ToList();  
        }

        public bool MemberExists(int id)
        {
            return _context.Members.Any(m => m.Id == id);
        }

        public bool CreateMember(Member m)
        {
            _context.Add(m);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false; 

        }

        public bool UpdateMember(Member m)
        {
            _context.Update(m);
            return Save();
        }

        public bool DeleteMember(Member m)
        {
            _context.Remove(m);
            return Save();
        }
    }
}
