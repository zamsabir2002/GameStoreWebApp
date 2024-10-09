using GamesMinimalApi.Data;
using GamesMinimalApi.Endpoints;
using GamesMinimalApi.GameDtos;

var builder = WebApplication.CreateBuilder(args);

//var connString = "Data Source=GameStore.db"; // SQLite directly on the root of project
var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString); // Dependency Injection

var app = builder.Build();

app.MapGamesEndpoints();

app.MapGenreEndpoint();

await app.MigrateDbAsync();

//app.MapGet("/", () => "Hello World!");

app.Run();
