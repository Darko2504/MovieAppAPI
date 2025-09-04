using Domain.Enums;
using Domain.Models;

namespace DataAccess.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MovieRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Movie entity)
        {
            _dbContext.Movies.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Movie entity)
        {
            _dbContext.Movies.Remove(entity);
            _dbContext.SaveChanges();
        }

        public List<Movie> FilterMovies(int? year, GenreEnum? genre)
        {
            if (year == null && genre == null)
            {
                return _dbContext.Movies.ToList();
            }

            if (year == null)
            {
                List<Movie> movieDb = _dbContext.Movies.Where(x => x.Genre == (GenreEnum)genre).ToList();
                return movieDb;
            }

            if (genre == null)
            {
                List<Movie> movieDb = _dbContext.Movies.Where(x => x.Year == year).ToList();
                return movieDb;
            }

            List<Movie> movies = _dbContext.Movies.Where(x =>
                                                        x.Year == year &&
                                                        x.Genre == (GenreEnum)genre
                                                        ).ToList();

            return movies;
        }

        public List<Movie> GetAll()
        {
            return _dbContext.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            return _dbContext.Movies.SingleOrDefault(x => x.Id == id);
        }

        public void Update(Movie entity)
        {
            _dbContext.Movies.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}

