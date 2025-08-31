using Animes.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Queries
{
    public class GetAnimeByIdQuery: IRequest<ResponseAnime>
    {
        public int Id {  get; set; }

        public GetAnimeByIdQuery(int id)
        {
            Id = id;
        }

    }
}
