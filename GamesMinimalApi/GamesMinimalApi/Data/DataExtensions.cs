using Microsoft.EntityFrameworkCore;

namespace GamesMinimalApi.Data
{
    public static class DataExtensions
    {
        public static async Task MigrateDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope(); // can use to request the service container of asp.net core to give us instance to some services regiestered in this app

            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
