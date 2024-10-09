using GamesMinimalApi.Entities;
using Microsoft.EntityFrameworkCore;

// DB context
namespace GamesMinimalApi.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
    {
        // DbContext : an object representing session with a database
        // can be used to query and save instances of your entity
        // an intermediatary

        // DBContext combination of both repository and unit of work pattern

        // DBSet to query and save instances of Game
        // LINQ queries against DBSET (here Games) will be translated to DB queries
        public DbSet<Game> Games => Set<Game>(); // To provide and initial value point directly into Set of type Game

        public DbSet<Genre> Genres => Set<Genre>();


        // Add new method to populate genres at startup once
        // This method is executed as soon as migrations are executed
        // For simple data population (seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new{ Id = 1, Name = "Fighting"},
                new{ Id = 2, Name = "RolePlaying"},
                new{ Id = 3, Name = "Sports"},
                new{ Id = 4, Name = "Racing"},
                new{ Id = 5, Name = "Kids and Family"}
            );
        }
    }
}
