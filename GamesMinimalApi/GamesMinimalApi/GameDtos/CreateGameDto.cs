using System.ComponentModel.DataAnnotations;

namespace GamesMinimalApi.GameDtos
{
    public record class CreateGameDto(
        [Required][StringLength(50)] string Name,
        int GenreId,
        //[Required][StringLength(20)] string Genre,
        [Required][Range(1,1000)] decimal Price,
        DateOnly ReleaseDate
    );
}
