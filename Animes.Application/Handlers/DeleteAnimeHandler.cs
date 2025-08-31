using Animes.Application.Commands;
using Animes.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Handlers
{
    public class DeleteAnimeHandler : IRequestHandler<DeleteAnimeCommand, Unit>
    {

        private readonly IAnimeRepository _animeRepository;

        public DeleteAnimeHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<Unit> Handle(DeleteAnimeCommand command, CancellationToken cancellationToken)
        {
            var anime = await _animeRepository.GetAnimeByID(command.Id);

            if (anime == null)
            {
                throw new ArgumentException("Anime nao encontrado");
            }

            await _animeRepository.DeleteAnime(anime);

            return Unit.Value;
        }

    }
}
