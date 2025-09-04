using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(User entity)
        {
            _db.Users.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(User entity)
        {
            _db.Users.Remove(entity);
            _db.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _db.Users.Include(x => x.MovieList).ToList();
        }

        public User GetById(int id)
        {
            return _db.Users.SingleOrDefault(x => x.Id == id);

        }

        public User GetUserByUsername(string username)
        {
            return _db.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower());
        }

        public User LoginUser(string username, string hashedPassword)
        {
            return _db.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == hashedPassword);
        }

        public void Update(User entity)
        {
            _db.Users.Update(entity);
            _db.SaveChanges();
        }
    }
}
