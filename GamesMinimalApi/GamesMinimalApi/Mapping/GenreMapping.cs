using GamesMinimalApi.Entities;
using GamesMinimalApi.GenreDtos;

namespace GamesMinimalApi.Mapping
{
    public static class GenreMapping
    {
        public static Genre ToEntity(this CreateGenreDto genreDto)
        {
            return new Genre()
            {
                Name = genreDto.Name
            };
        }
        public static GenreDto ToDto(this Genre genre)
        {
            return new(
                genre.Id,
                genre.Name
            );
        }

        public static Genre ToEntity(this UpdateGenreDto genreDto,int id)
        {
            return new Genre()
            {
                Id = id,
                Name = genreDto.Name
            };
        }
    }
}
