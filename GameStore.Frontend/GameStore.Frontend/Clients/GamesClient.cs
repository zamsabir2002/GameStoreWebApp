using GameStore.Frontend.Models;
using System.Text.Json.Nodes;

namespace GameStore.Frontend.Clients
{
    public class GamesClient(HttpClient httpClient)
    {
        //private readonly List<GameSummary> games = 
        //[
        //    new (){
        //       Id = 1,
        //       Name = "Street Fighet XI",
        //       Genre = "Fighting",
        //       Price = 9.99M,
        //       ReleaseDate = new DateOnly(1992,7,15)
        //    },
        //    new (){
        //       Id = 2,
        //       Name = "FIFA 2k13",
        //       Genre = "Sports",
        //       Price = 10.99M,
        //       ReleaseDate = new DateOnly(2012,11,3)
        //    },
        //    new (){
        //       Id = 3,
        //       Name = "Final Fantasy XIV",
        //       Genre = "RolePlay",
        //       Price = 7.50M,
        //       ReleaseDate = new DateOnly(2022,3,26)
        //    },
        //];

        //private readonly Genre[] genres = new GenreClient(httpClient).GetGenres();


        //public GameSummary[] GetGames() => [.. games];
        // changed to get games async
        
        
        public async Task<GameSummary[]> GetGamesAsync()
        {
            // in this case where one async func GetGames directly invokes 
            // another async httpClient.... you can remove async and await
            // keywords and it will work fine
           return await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];
        }


        //public GameDetails GetGame(int id)
        //{
        //    GameSummary? game = GetGameSummaryById(id);

        //    var genre = genres.Single(genre => string.Equals(
        //        genre.Name,
        //        game.Genre,
        //        StringComparison.OrdinalIgnoreCase
        //    ));

        //    return new GameDetails
        //    {
        //        Id = game.Id,
        //        Name = game.Name,
        //        Price = game.Price,
        //        GenreId = genre.Id.ToString(),
        //        ReleaseDate = game.ReleaseDate
        //    };
        //}
        public async Task<GameDetails> GetGameAsync(int id) => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") 
            ?? throw new Exception("Could not find game!");

        public async Task AddGameAsync(GameDetails newGame) => await httpClient.PostAsJsonAsync("/games", newGame);

        //public void UpdateGame(GameDetails updatedGame)
        //{
        //    //var index = games.FindIndex(game => game.Id == id);

        //    //Genre genre = GetGenrebyId(updatedGame.GenreId);

        //    //games[index] = new GameSummary()
        //    //{
        //    //    Id = id,
        //    //    Name = updatedGame.Name,
        //    //    Price = updatedGame.Price,
        //    //    Genre = genre.Name,
        //    //    ReleaseDate = updatedGame.ReleaseDate
        //    //};

        //    Genre genre = GetGenrebyId(updatedGame.GenreId);

        //    GameSummary existingGame = GetGameSummaryById(updatedGame.Id);

        //    existingGame.Name = updatedGame.Name;
        //    existingGame.Price = updatedGame.Price;
        //    existingGame.Genre = genre.Name;
        //    existingGame.ReleaseDate = updatedGame.ReleaseDate;
        //}

        public async Task UpdateGameAsync(GameDetails updatedGame) => await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}",updatedGame);

        //public void DeleteGame(GameSummary game)
        //{
        //    games.Remove(game);
        //}
        // Either above one or this one
        //public void DeleteGame(int id)
        //{
        //    var game = GetGameSummaryById(id);
        //    games.Remove(game);
        //}

        public async Task DeleteGameAsync(int id) => await httpClient.DeleteAsync($"games/{id}");

        
        //private GameSummary GetGameSummaryById(int id)
        //{
        //    GameSummary? game = games.Find(game => game.Id == id);
        //    ArgumentNullException.ThrowIfNull(game);
        //    return game;
        //}

        //private Genre GetGenrebyId(string? id)
        //{
        //    ArgumentException.ThrowIfNullOrWhiteSpace(id);
        //    return genres.Single((genre) => genre.Id == int.Parse(id));
        //}

    }
}
