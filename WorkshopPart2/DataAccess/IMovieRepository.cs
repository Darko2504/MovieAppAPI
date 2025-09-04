using Domain.Enums;
using Domain.Models;

namespace DataAccess
{
    public interface IMovieRepository : IRepository<Movie>
    {
        List<Movie> FilterMovies(int? year, GenreEnum? genre);
    }
}
