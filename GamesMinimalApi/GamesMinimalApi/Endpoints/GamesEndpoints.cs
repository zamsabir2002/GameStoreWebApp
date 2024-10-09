using GamesMinimalApi.Data;
using GamesMinimalApi.Entities;
using GamesMinimalApi.GameDtos;
using GamesMinimalApi.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GamesMinimalApi.Endpoints
{
    public static class GamesEndpoints
    {
        //public static List<GameSummaryDto> games = [
        //new (
        //    1,
        //    "Street Fighet XI",
        //    "Fighting",
        //    99.99M,
        //    new DateOnly(2012,12,3)),
        //new (
        //    2,
        //    "Megaman X8",
        //    "Fantasy",
        //    99.99M,
        //    new DateOnly(2008,5,12)),
        //new (
        //    3,
        //    "Dota",
        //    "Role Play",
        //    99.99M,
        //    new DateOnly(2009,3,5))
        //];

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("games")
                .WithParameterValidation();

            // GET /games
            group.MapGet("/", async (GameStoreContext dbContext) => {


                return await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => game.ToGameSummaryDto())
                .AsNoTracking() // Avoid tracking for optimization -- tracking is from context
                .ToListAsync();
            });


            // GET /games/1
            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => {

                //GameDto? game = games.Find((GameDto game) => game.Id == id);

                Game? game = await dbContext.Games.FindAsync(id);


                return game is null ? 
                Results.NotFound($"No Game with Id {id}") : Results.Ok(game.ToGameDetailsDto());

            }).WithName("GetGame");


            // POST /games
            group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) => {

                Game game = newGame.ToEntity();

                // EF will assign the genre ID now with seperate DTOs
                //game.Genre = dbContext.Genres.Find(newGame.GenreId);

                //dbContext.Add(game); // Will Work
                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync(); // Save and trasnfrom to sql to make changes

                //GameDto game = new GameDto(
                //    games.Count + 1,
                //    newGame.Name,
                //    newGame.Genre,
                //    newGame.Price,
                //    newGame.ReleaseDate);

                //games.Add(game);

                // to return back to client
                //GameDto gameDto = new(
                //    game.Id,
                //    game.Name,
                //    game.Genre!.Name,
                //    game.Price,
                //    game.ReleaseDate
                //);

                return Results.CreatedAtRoute(
                    "GetGame", 
                    new { id = game.Id }, 
                    game.ToGameDetailsDto()
                );
            });


            group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => {

                //int index = games.FindIndex(games => games.Id == id);
                //if (index == -1)
                //{
                //    return Results.NotFound($"No Game with Id {id}");
                //}
                //games[index] = new GameSummaryDto(
                //    id,
                //    updatedGame.Name,
                //    updatedGame.Genre,
                //    updatedGame.Price,
                //    updatedGame.ReleaseDate
                //);


                var existingGame = await dbContext.Games.FindAsync(id);
                if (existingGame == null)
                {
                    return Results.NotFound($"No Game with Id {id}");
                }
                dbContext.Entry(existingGame)
                    .CurrentValues
                    .SetValues(updatedGame.ToEntity(id));

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });


            group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) => {

                // Old
                //int index = games.FindIndex(games => games.Id == id);
                //games.RemoveAll(games => games.Id == id);

                // Batch Delete Operation
                await dbContext.Games
                    .Where(game => game.Id == id)
                    .ExecuteDeleteAsync();
                

                return Results.NoContent();
            });

            return group;
        }

    }
}
