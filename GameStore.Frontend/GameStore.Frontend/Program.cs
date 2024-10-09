using GameStore.Frontend.Clients;
using GameStore.Frontend.Components;



var builder = WebApplication.CreateBuilder(args);

var gameStoreApiUrl = builder.Configuration["GameStoreApiUrl"] ??
    throw new Exception("GameStoreAPI URL is not set");

// Add services to the container.
builder.Services.AddRazorComponents() // also allows navigation manager
                .AddInteractiveServerComponents(); 

// lifetime of both http and games client will be scoped to that of the websocket connection created between server and the browser
// closing and opening new browser would mean new service
builder.Services.AddHttpClient<GamesClient>(
    client => client.BaseAddress = new Uri(gameStoreApiUrl) 
); // register both http client and GameClient at the same time

builder.Services.AddHttpClient<GenreClient>(
    client => client.BaseAddress = new Uri(gameStoreApiUrl) 
);

// same and one instance across entire lifetime of application
//builder.Services.AddSingleton<GamesClient>();
//builder.Services.AddSingleton<GenreClient>();
// registered by the http client

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// To remove warning in terminal 
// And to avoid app trying to redirect to Https
//app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>() 
   .AddInteractiveServerRenderMode();

app.Run();
