using System.ComponentModel.DataAnnotations;

namespace GamesMinimalApi.GenreDtos
{
    public record class CreateGenreDto(
        [Required][StringLength(20)] string Name    
    );
}
