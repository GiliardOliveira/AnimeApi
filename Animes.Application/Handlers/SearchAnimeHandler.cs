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
    public class SearchAnimeHandler: IRequestHandler<SearchAnimeQuery, List<ResponseAnime>>
    {
        private readonly IAnimeRepository _animeRepository;

        public SearchAnimeHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public async Task<List<ResponseAnime>> Handle (SearchAnimeQuery query, CancellationToken cancellationToken)
        {
            var animes = await _animeRepository.SearchAnime(
                id: query.Id,
                nome: query.Name,
                diretor: query.Diretor
             );

            return animes.Select(a => new ResponseAnime
            {
                Id = a.Id,
                Nome = a.Name,
                Diretor = a.Diretor,
                Resumo =  a.Resume
            }).ToList();
        }
    }
}
