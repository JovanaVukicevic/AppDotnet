using AppDotnet.Data;
using AppDotnet.Interfaces;
using AppDotnet.Models;

namespace AppDotnet.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
                _context = context;
        }

        public bool CreateGenre(Genre genre)
        {
            _context.Add(genre);
            return Save();
        }

        public bool GenreExists(int id)
        {
            return _context.Genres.Any(g => g.Id == id);
        }

        public Genre GetGenre(int id)
        {
            return _context.Genres.Where(g => g.Id == id).FirstOrDefault();
        }

        public Genre GetGenreByName(string name)
        {
            return _context.Genres.Where(g => g.Name == name).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
