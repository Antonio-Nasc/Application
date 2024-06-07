using Anime.Application.Services;
using Anime.Domain.Entities;
using Anime.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.Test.Services
{
    public class AnimeServiceTests
    {
        [Fact]
        public async Task GetAllAnimesAsync_ReturnsAllAnimes()
        {
            var mockRepo = new Mock<IAnimeRepository>();
            mockRepo.Setup(repo => repo.GetAllAnimesAsync()).ReturnsAsync(GetSampleAnimes());
            var service = new AnimeService(mockRepo.Object);

            var result = await service.GetAllAnimesAsync();

            Assert.Equal(2, result.Count());
        }

        private List<Animee> GetSampleAnimes()
        {
            return new List<Animee>
            {
                new Animee { Id = 1, Name = "One Piece", Summary = "Pirata em busca do tesouro", Director = "Eiichiro Oda" },
                new Animee { Id = 2, Name = "Dragon ball Z", Summary = "Guerreiros Z lutando para proteger a terra dos inimigos", Director = "Akira Toriyama" }
            };
        }
    }
}
