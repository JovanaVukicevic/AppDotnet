using AppDotnet.Data;
using AppDotnet.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;

namespace AppDotnet
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            var Books = new List<Book>()
            {
            new Book()
            {
                Title = "Dune",
                Author = "Frank Herbert",
                Genre = new Genre()
                {
                    Name = "Sci-fi"
                }
            },
            new Book()
            {
                Title = "Dovoljno je malo sunca",
                Author = "Vitomirka Trebovac",
                Genre = new Genre()
                {
                    Name = "Poetry"
                }
            },
            new Book()
            {
                Title = "Waiting for Godot",
                Author = "Samuel Beckett",
                Genre = new Genre()
                {
                    Name = "Drama"
                }
            }
        };
            var Members = new List<Member>() {
            new Member()
                        {
                            FirstName = "Marko",
                            LastName = "Petrovic",
                            Adress = "Trg boraca",
                            BirthDate =new DateOnly(2000, 8, 5),
                            PhoneNumber = "068364728",
                            Borrowings = new List<Borrowing>()
                            {
                                new Borrowing() {
                                 MemberId = 1,
                                BookId =1,
                                BorrowDate = new DateOnly(2024, 4, 4),
                                Returned = false}
                            }
                        },
            new Member()
            {
                FirstName = "Marija",
                LastName = "Rakocevic",
                Adress = "Ulica 13. jula",
                BirthDate = new DateOnly(2002, 9, 3),
                PhoneNumber = "068364999",
                Borrowings = new List<Borrowing>(){
                    new Borrowing()
                             {
                                 MemberId = 2,
                                 BookId = 2,
                                 BorrowDate = new DateOnly(2024, 5, 1),
                                 Returned = false
                             }
                }
            },
            new Member()
            {
                FirstName = "Milica",
                LastName = "Scepanovic",
                Adress = "Dunje DJokic 2",
                BirthDate = new DateOnly(1998, 10, 2),
                PhoneNumber = "068384932",
                Borrowings = new List<Borrowing>()
                {
                    new Borrowing() {
                        MemberId = 3,
                        BookId = 3,
                        BorrowDate = new DateOnly(2024, 2, 5),
                        Returned = false
                            }
            }
            }
        };
            
           
            
            


            dataContext.Members.AddRange(Members);
            dataContext.Books.AddRange(Books);
            dataContext.SaveChanges();
            
        }
    }
}
