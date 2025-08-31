using Animes.Application.DTO;
using Animes.Application.Queries;
using Animes.Domain.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Handlers
{
    public class GetAnimeByIdHandler: IRequestHandler<GetAnimeByIdQuery,ResponseAnime>
    {
        private readonly IAnimeRepository _animeRepository;

        public GetAnimeByIdHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<ResponseAnime> Handle(GetAnimeByIdQuery request,CancellationToken cancellationToken)
        {
            var anime = await _animeRepository.GetAnimeByID(request.Id);

            if(anime == null)
            {
                throw new ArgumentException("Anime nao encontrado");
            }

            return new ResponseAnime
            {
                Id = anime.Id,
                Nome = anime.Name,
                Diretor = anime.Diretor,
                Resumo = anime.Resume
            };
        }
    }
}
