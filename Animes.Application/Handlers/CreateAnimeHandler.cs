using Animes.Application.Commands;
using Animes.Domain.Entities;
using Animes.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Handlers
{
    public class CreateAnimeHandler: IRequestHandler<CreateAnimeCommand,Unit>
    {
        private readonly IAnimeRepository _animeRepository;

        public CreateAnimeHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }


        public async Task<Unit> Handle(CreateAnimeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name)) {
                throw new ArgumentException("Nome nao pode ser Vazio");
            }

            var anime = new Anime
            {
                Name = request.Name,
                Diretor = request.Director,
                Resume = request.Resume
            };
            await _animeRepository.AddAnime(anime);
            return Unit.Value;
        }
    }
}
