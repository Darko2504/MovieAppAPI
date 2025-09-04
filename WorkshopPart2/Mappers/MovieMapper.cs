using Domain.Models;
using Dtos.Dtos;

namespace Mappers
{
    public static class MovieMapper
    {
        public static Movie ToMovie(this CreateMovieDto addMovieDto)
        {
            return new Movie
            {
                Year = addMovieDto.Year,
                Title = addMovieDto.Title,
                Genre = addMovieDto.Genre,
                Description = addMovieDto.Description
            };
        }

        public static MovieDto ToMovieDto(this Movie movie)
        {
            return new MovieDto
            {
                Year = movie.Year,
                Title = movie.Title,
                Genre = movie.Genre,
                Description = movie.Description
            };
        }
    }
}

