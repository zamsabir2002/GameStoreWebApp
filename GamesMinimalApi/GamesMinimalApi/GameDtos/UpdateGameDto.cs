using System.ComponentModel.DataAnnotations;

namespace GamesMinimalApi.GameDtos
{
    public record class UpdateGameDto(
        [Required][StringLength(50)] string Name,
        int GenreId,
        [Required][Range(1, 1000)] decimal Price,
        DateOnly ReleaseDate
    );
}
