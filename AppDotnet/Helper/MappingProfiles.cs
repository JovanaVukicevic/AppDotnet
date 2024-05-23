using AppDotnet.Dto;
using AppDotnet.Models;
using AutoMapper;

namespace AppDotnet.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Member, MemberDto>();
            CreateMap<MemberDto, Member>();
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<Borrowing, BorrowingDto>();
            CreateMap<BorrowingDto, Borrowing>();
            CreateMap<Genre, GenreDto>();
            CreateMap<GenreDto, Genre>();

        }
    }
}
