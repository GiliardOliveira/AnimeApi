using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Domain.Entities
{
    public class Anime
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Diretor { get; set; }
        public string Resume { get; set; }

        public Anime() { }

        public Anime(int id, string name, string diretor, string resume)
        {
            Id = id;
            Name = name;
            Diretor = diretor;
            Resume = resume;
        }
    }
}
