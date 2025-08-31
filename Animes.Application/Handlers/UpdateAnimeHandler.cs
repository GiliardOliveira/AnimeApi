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
    public class UpdateAnimeHandler: IRequestHandler<UpdateAnimeCommand,Unit>
    {
        private readonly IAnimeRepository _animeRepository;

        public UpdateAnimeHandler(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }


        public async Task<Unit> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
        {
            var anime= await _animeRepository.GetAnimeByID(request.Id);

            if (anime == null) 
            { 
                throw new ArgumentException("Anime nao encontrado");
            }

            anime.Name = request.Name;
            anime.Diretor = request.Diretor;
            anime.Resume = request.Resumo;


            await _animeRepository.UpdateAnime(anime);
            return Unit.Value;
        }
    }
}
