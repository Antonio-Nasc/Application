using Anime.Domain.Entities;
using Anime.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.Application.Services
{
    public class AnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public Task<Animee> GetAnimeByIdAsync(int id) => _animeRepository.GetAnimeByIdAsync(id);

        public Task<IEnumerable<Animee>> GetAllAnimesAsync() => _animeRepository.GetAllAnimesAsync();

        public Task AddAnimeAsync(Animee anime) => _animeRepository.AddAnimeAsync(anime);

        public Task UpdateAnimeAsync(Animee anime) => _animeRepository.UpdateAnimeAsync(anime);

        public Task DeleteAnimeAsync(int id) => _animeRepository.DeleteAnimeAsync(id);
    }
}
