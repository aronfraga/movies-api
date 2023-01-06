using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Data {
    public class Context : DbContext {
        
        public Context(DbContextOptions<Context> options) : base(options) { 

        }

        public DbSet<Category> Categorys { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
