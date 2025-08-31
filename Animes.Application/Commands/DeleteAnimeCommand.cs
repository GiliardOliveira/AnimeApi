using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Application.Commands
{
    public class DeleteAnimeCommand: IRequest<Unit>
    {
        public int Id { get; set; }

        public DeleteAnimeCommand(int id) {  
            
           Id = id; 
        
        }
    }
}
