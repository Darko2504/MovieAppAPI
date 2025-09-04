using DataAccess;
using Domain.Enums;
using Domain.Models;
using Dtos.Dtos;
using Mappers;
using Services.Interfaces;
using Shared;

namespace Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository; 
        }
        public void AddMovie(CreateMovieDto addMovieDto)
        {
            if (string.IsNullOrEmpty(addMovieDto.Title))
            {
                throw new MovieException("Title cannot be empty");
            }
            if (addMovieDto.Description.Length > 250)
            {
                throw new MovieException("Description can not be longer than 250 words");
            }
            if (addMovieDto.Year <= 0)
            {
                throw new MovieException("Year must have positive value");
            }

            Movie newMovie = addMovieDto.ToMovie();

            _movieRepository.Add(newMovie);
        }

        public void DeleteMovie(int id)
        {
            if (id < 0)
            {
                throw new MovieException("Cannot enter negative id");
            }

            var movieDb = _movieRepository.GetById(id);
            if (movieDb == null)
            {
                throw new MovieNotFoundException("Movie not found! Try again");
            }
            _movieRepository.Delete(movieDb);
        }

        public List<MovieDto> FilterMovies(int? year, GenreEnum? genre)
        {
            if (genre.HasValue)
            {
                var enumValues = Enum.GetValues(typeof(GenreEnum)).Cast<GenreEnum>().ToList();
                if (!enumValues.Contains(genre.Value))
                {
                    throw new MovieException("Genre does not exist. Try again!");
                }
            }

            return _movieRepository.FilterMovies(year, genre).Select(x => x.ToMovieDto()).ToList();
        }

        public List<MovieDto> GetAllMovies()

        {
            var moviesDbo =  _movieRepository.GetAll();
            if (moviesDbo.Count() <= 0)
            {
                throw new MovieException("Movies object is empty!");
            }
            else
            {
                return moviesDbo.Select(x => x.ToMovieDto()).ToList();   
            }
        }

        public MovieDto GetMovieById(int id)
        {
            if (id <= 0)
            {
                throw new MovieException("The id must have positive value!");
            }
            var movieDb = _movieRepository.GetById(id);
            if (movieDb == null)
            {
                throw new MovieNotFoundException("Movie does not exist!");
            }
            return movieDb.ToMovieDto();
        }

        public void UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            Movie movieDb = _movieRepository.GetById(updateMovieDto.Id);
            if (movieDb == null)
                throw new MovieNotFoundException("Movie was not found");

            if (string.IsNullOrEmpty(updateMovieDto.Title))
            {
                throw new MovieException("Title must not be empty");
            }
            if (updateMovieDto.Year <= 0)
            {
                throw new MovieException("Year must have positive value");
            }
            if (updateMovieDto.Description.Length > 250)
            {
                throw new MovieException("Description can not be longer than 250 words");
            }

            movieDb.Year = updateMovieDto.Year;
            movieDb.Title = updateMovieDto.Title;
            movieDb.Description = updateMovieDto.Description;
            movieDb.Genre = updateMovieDto.Genre;

            _movieRepository.Update(movieDb);
        }
    }
}
