using GamesMinimalApi.Data;
using GamesMinimalApi.Entities;
using GamesMinimalApi.GenreDtos;
using GamesMinimalApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GamesMinimalApi.Endpoints
{
    public static class GenresEndpoint
    {
        public static RouteGroupBuilder MapGenreEndpoint(this WebApplication app)
        {
            var genreGroup = app.MapGroup("genre")
                .WithParameterValidation();

            //genreGroup.MapGet("/", (GameStoreContext dbContext) =>
            //    dbContext.Genres.Select((genre) => new GenreDto(genre.Id, genre.Name))
            //);

            genreGroup.MapGet("/", async (GameStoreContext dbContext) =>
                await dbContext.Genres
                .Select(genre => genre.ToDto())
                .AsNoTracking()
                .ToListAsync()
            );            
            
            genreGroup.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {
                var genre = await dbContext.Genres.FindAsync(id);
                if (genre == null)
                {
                    return Results.NotFound($"No Genre With Id {id}");
                }
                return Results.Ok(genre.ToDto());
            }).WithName("GetGenre");

            genreGroup.MapPost("/", async (CreateGenreDto newGenre, GameStoreContext dbContext) =>
            {
                Genre genre = newGenre.ToEntity();

                dbContext.Add(genre);
                await dbContext.SaveChangesAsync();

                return Results.CreatedAtRoute(
                "GetGenre",
                new { id = genre.Id },
                genre.ToDto()
                );
            });

            genreGroup.MapPut("/{id}", async (int id, UpdateGenreDto updatedGenre, GameStoreContext dbContext) =>
            {
                var existingGenre = await dbContext.Genres.FindAsync(id);

                if (existingGenre == null)
                {
                    return Results.NotFound();
                }

                dbContext.Entry(existingGenre)
                    .CurrentValues
                    .SetValues(updatedGenre.ToEntity(id));

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });

            genreGroup.MapDelete("/{id}", async(int id, GameStoreContext dbContext) =>
            {
                await dbContext.Genres
                    .Where(genre => genre.Id == id)
                    .ExecuteDeleteAsync();

                return Results.NoContent();
            });

            return genreGroup;
        }
    }
}
