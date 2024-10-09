using System.ComponentModel.DataAnnotations;

namespace GamesMinimalApi.GenreDtos
{
    public record class UpdateGenreDto(
        [Required][StringLength(20)] string Name
    );
}
