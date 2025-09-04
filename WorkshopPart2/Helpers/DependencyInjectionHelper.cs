using DataAccess;
using DataAccess.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Implementations;
using Services.Interfaces;

namespace Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection service)
        {
            service.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer("Server=DARKOTUF\\SQLEXPRESS;Database=NotesAppNew;Trusted_Connection=True;TrustServerCertificate=True;"));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>(); 
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IUserService, UserService>();
        }

    }
}

