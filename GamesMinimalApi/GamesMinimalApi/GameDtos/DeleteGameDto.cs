using System.ComponentModel.DataAnnotations;

namespace GamesMinimalApi.GameDtos
{
    public record class DeleteGameDto(
        int Id,
        string Name,
        string Genre,
        decimal Price,
        DateOnly ReleaseDate
    );
}
