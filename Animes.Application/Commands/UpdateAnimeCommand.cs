using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Commands
{
    public class UpdateAnimeCommand :IRequest<Unit>
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Diretor { get; set; }
        public string Resumo { get; set; }

        public UpdateAnimeCommand(int id,string name, string diretor,string resumo)
        {
            Id = id;
            Name = name;
            Diretor = diretor;
            Resumo = resumo;
        }
    }
}
