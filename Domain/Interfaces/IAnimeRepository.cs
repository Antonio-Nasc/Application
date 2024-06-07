using Anime.Domain.Entities;

namespace Anime.Domain.Interfaces
{
    public interface IAnimeRepository
    {
        Task<Animee> GetAnimeByIdAsync(int id);
        Task<IEnumerable<Animee>> GetAllAnimesAsync();
        Task AddAnimeAsync(Animee anime);
        Task UpdateAnimeAsync(Animee anime);
        Task DeleteAnimeAsync(int id);
    }
}
