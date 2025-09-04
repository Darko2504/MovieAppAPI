using Domain.Enums;
using Dtos.Dtos;

namespace Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies();
        List<MovieDto> FilterMovies(int? year, GenreEnum? genre);
        MovieDto GetMovieById(int id);
        void AddMovie(CreateMovieDto addMovieDto);
        void UpdateMovie(UpdateMovieDto updateMovieDto);
        void DeleteMovie(int id);
    }
}
