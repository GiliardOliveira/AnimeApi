using Animes.Application.Commands;
using Animes.Application.DTO;
using Animes.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimesApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AnimeController> _logger;

        
        public AnimeController(IMediator mediator, ILogger<AnimeController> logger)
        {
            _mediator =mediator;
            _logger = logger;
        }


        /// <summary>
        /// Adiciona um novo anime à coleção.
        /// </summary>
        /// <remarks>
        /// Este endpoint recebe um objeto com os detalhes do anime e o salva no banco de dados.
        /// Um exemplo de corpossssss da requisição é mostrado abaixo.
        ///
        ///     POST /api/Animes
        ///     {
        ///        "name": "Death Note",
        ///        "director": "Tetsuro Araki",
        ///        "resume": "Um estudante encontra um caderno que pode matar pessoas."
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnimeCommand anime)
        {
            try
            {
                await _mediator.Send(anime);
                _logger.LogInformation("Anime criado com sucesso: {Nome}", anime.Name);
                return StatusCode(201);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar anime: {Nome}", anime.Name);
                return StatusCode(500, "Erro interno do servidor");
            }
        }


        /// <summary>
        /// Exclui um anime existente.
        /// </summary>
        /// <param name="id">O ID do anime a ser excluído.</param>
        /// <returns>Uma resposta vazia indicando o sucesso da operação.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var delete = new DeleteAnimeCommand(id);


                await _mediator.Send(delete);
                _logger.LogInformation("Anime excluído com sucesso: {Id}", id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation("Anime nao encontrado para o id: {id}",id);
                return NotFound();
            }
        }

        /// <summary>
        /// Atualiza um anime existente.
        /// </summary>
        /// <remarks>
        /// O método busca um anime pelo ID fornecido na URL e o atualiza com os dados do corpo da requisição.
        /// Se o anime não for encontrado, retorna uma resposta 404.
        /// </remarks>
        /// <param name="id">O ID do anime a ser atualizado.</param>
        /// <param name="animeUp">O objeto com os dados atualizados para o anime.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAnimeRequest animeUp)
        {

            try
            {
                var updatecommand = new UpdateAnimeCommand(
                    id,
                    animeUp.Nome,
                    animeUp.Diretor,
                    animeUp.Resumo
                    );

                await _mediator.Send(updatecommand);

                _logger.LogInformation("Anime atualizado com sucesso: {Id}", id);
                return NoContent();

            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation($"Anime nao encontrado para o id: {id}");
                return NotFound();
            }
        }


        /// <summary>
        /// Permite buscar animes por múltiplos critérios (ID, nome ou diretor).
        /// </summary>
        /// <param name="id">O ID do anime a ser buscado.</param>
        /// <param name="nome">O nome do anime (ou parte dele) a ser buscado.</param>
        /// <param name="diretor">O nome do diretor (ou parte dele) a ser buscado.</param>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] int? id = null, [FromQuery] string? nome = null, [FromQuery] string? diretor=null)
        {
            try
            {

                var query = new SearchAnimeQuery(id, nome, diretor);

                var animes = await _mediator.Send(query);
                
                return Ok(animes);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Erro ao buscar animes");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    }
}
