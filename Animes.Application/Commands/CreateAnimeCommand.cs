using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Commands
{
    public class CreateAnimeCommand: IRequest<Unit>
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public string Resume { get; set; }


        public CreateAnimeCommand(string name, string director, string resume)
        {
            Name = name;
            Director = director;
            Resume = resume;
        }
    }
}
