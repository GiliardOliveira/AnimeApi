using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.DTO
{
    public class ResponseAnime
    {
        public int Id {  get; set; }
        public string? Nome { get; set; }
        public string? Diretor { get; set; }
        public string? Resumo { get; set; }
    }
}
