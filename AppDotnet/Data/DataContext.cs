using AppDotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace AppDotnet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowings   { get; set; }
        public DbSet<Genre> Genres { get; set; }
        
    }
}
