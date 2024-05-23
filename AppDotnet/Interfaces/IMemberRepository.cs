using AppDotnet.Models;

namespace AppDotnet.Interfaces
{
    public interface IMemberRepository
    {
        public ICollection<Member> GetMembers  ();
        public Member GetMember (int id);
        public bool CreateMember (Member m);
        ICollection<Borrowing> GetBorrowings (int memId);
        public bool MemberExists(int id);
        bool UpdateMember(Member m);
        bool DeleteMember(Member m);
        bool Save();
    }
}
