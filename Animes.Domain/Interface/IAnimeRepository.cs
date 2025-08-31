using Animes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Domain.Interface
{
    public interface IAnimeRepository
    {
        Task<Anime> AddAnime(Anime anime);
        Task UpdateAnime(Anime anime);
        Task DeleteAnime(Anime anime);
        Task<IEnumerable<Anime>> GetAllAnimes();
        Task<Anime?> GetAnimeByID(int id);

        Task<IEnumerable<Anime>> SearchAnime(int? id=null, string? nome = null, string? diretor = null);
    }
}
