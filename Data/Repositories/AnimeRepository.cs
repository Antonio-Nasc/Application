using Anime.Data.Context;
using Anime.Domain.Entities;
using Anime.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.Data.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimeContext _context;

        public AnimeRepository(AnimeContext context)
        {
            _context = context;
        }

        public async Task<Animee> GetAnimeByIdAsync(int id)
        {
            return await _context.Animes.FindAsync(id);
        }

        public async Task<IEnumerable<Animee>> GetAllAnimesAsync()
        {
            return await _context.Animes.ToListAsync();
        }

        public async Task AddAnimeAsync(Animee anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAnimeAsync(Animee anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnimeAsync(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime != null)
            {
                _context.Animes.Remove(anime);
                await _context.SaveChangesAsync();
            }
        }
    }
}
