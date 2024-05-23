namespace AppDotnet.Dto
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
