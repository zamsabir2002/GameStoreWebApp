using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients
{
    public class GenreClient(HttpClient httpClient)
    {
        //private readonly Genre[] genres = 
        //[
        //    new(){
        //        Id = 1,
        //        Name= "Fighting"
        //    },
        //    new(){
        //        Id = 2,
        //        Name= "Roleplaying"
        //    },
        //    new(){
        //        Id = 3,
        //        Name= "Sports"
        //    },
        //    new(){
        //        Id = 4,
        //        Name= "Racing"
        //    },
        //    new(){
        //        Id = 5,
        //        Name= "Kids and Family"
        //    },
        //];

        public async Task<Genre[]> GetGenresAsync() => 
            await httpClient.GetFromJsonAsync<Genre[]>("genre") ?? [];
    }
}
