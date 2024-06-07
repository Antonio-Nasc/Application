using Anime.Application.Services;
using Anime.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Anime.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {
        private readonly AnimeService _animeService;
        private readonly ILogger<AnimeController> _logger;
        public AnimeController(AnimeService animeService, ILogger<AnimeController> logger)
        {
            _animeService = animeService;
            _logger = logger;
        }

        /// <summary>
        /// Cria um novo anime.
        /// </summary>
        /// <param name="anime">Dados do anime a ser criado.</param>
        /// <returns>O anime criado.</returns>
        [HttpPost]
        public async Task<ActionResult<Animee>> PostAnime(Animee anime)
        {
            await _animeService.AddAnimeAsync(anime);
            _logger.LogInformation("Novo anime criado: {AnimeId}, Nome: {AnimeName}", anime.Id, anime.Name);
            return CreatedAtAction(nameof(GetAnime), new { id = anime.Id }, anime);
        }

        /// <summary>
        /// Obtém um anime pelo seu ID
        /// </summary>
        /// <param name="id">ID do anime a ser obtido</param>
        /// <returns>O anime encontrado.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Animee>> GetAnime(int id)
        {
            var anime = await _animeService.GetAnimeByIdAsync(id);

            if (anime == null)
            {
                return NotFound();
            }

            return anime;
        }

        /// <summary>
        /// Obtém todos os animes
        /// </summary>
        /// <returns>Lista de todos os animes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animee>>> GetAnimes()
        {
            return Ok(await _animeService.GetAllAnimesAsync());
        }

        /// <summary>
        /// Atualiza um anime existente.
        /// </summary>
        /// <param name="id">ID do anime a ser atualizado</param>
        /// <param name="anime">Dados atualizados do anime.</param>
        /// <returns>Nenhuma informação de retorno</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnime(int id, Animee anime)
        {
            if (id != anime.Id)
            {
                _logger.LogError("Erro ao atualizar o anime. O ID fornecido ({Id}) não corresponde ao ID do anime ({AnimeId})", id, anime.Id);
                return BadRequest();
            }

            await _animeService.UpdateAnimeAsync(anime);
            _logger.LogInformation("Anime atualizado com sucesso. ID: {AnimeId}, Nome: {AnimeName}", anime.Id, anime.Name);
            return NoContent();
        }

        /// <summary>
        /// Remove um anime pelo seu ID.
        /// </summary>
        /// <param name="id">ID do anime a ser removido.</param>
        /// <returns>Nenhuma informação de retorno.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            await _animeService.DeleteAnimeAsync(id);
            _logger.LogInformation("Anime removido com sucesso. ID: {AnimeId}", id);
            return NoContent();
        }
    }
}
