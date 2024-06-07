using Anime.API.Controllers;
using Anime.Application.Services;
using Anime.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anime.Test.Controllers
{
    public class AnimeControllerTests
    {
        [Fact]
        public async Task GetAnimes_ReturnsOkResult_WithListOfAnimes()
        {
            var mockService = new Mock<AnimeService>(null);
            mockService.Setup(service => service.GetAllAnimesAsync()).ReturnsAsync(GetSampleAnimes());
            var controller = new AnimeController(mockService.Object, null);

            var result = await controller.GetAnimes();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Animee>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
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
