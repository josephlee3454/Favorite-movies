using Microsoft.EntityFrameworkCore;

namespace favoriteMovies.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
    
        public DbSet<User> Users {get;set;}

        public DbSet<Movie> Movies {get;set;}

        public DbSet<Like> Likes {get;set;}

       
        
    }
}