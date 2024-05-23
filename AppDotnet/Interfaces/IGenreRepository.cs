using AppDotnet.Models;

namespace AppDotnet.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();
        Genre GetGenre(int id);
        Genre GetGenreByName(string name);
        bool CreateGenre(Genre genre);
        bool Save();
        bool GenreExists(int id);
        
    }
}
