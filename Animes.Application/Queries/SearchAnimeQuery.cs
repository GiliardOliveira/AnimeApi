using Animes.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Queries
{
    public class SearchAnimeQuery : IRequest<List<ResponseAnime>>
    {
       public int ? Id { get; set; }
       public string? Name { get; set; }
       public string? Diretor {  get; set; }

        public SearchAnimeQuery(int? id, string? name, string? diretor)
        {
            Id = id;
            Name = name;
            Diretor = diretor;
        }
    }
}
