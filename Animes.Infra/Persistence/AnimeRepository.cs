using Animes.Domain.Entities;
using Animes.Domain.Interface;
using Animes.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Infra.Persistence
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimeDbContext _db;

        public AnimeRepository(AnimeDbContext db)
        {
            _db = db;
        }

        public async Task<Anime> AddAnime(Anime anime)
        {
            await _db.Animes.AddAsync(anime);
            await _db.SaveChangesAsync();
            return anime;
        }

        public async Task DeleteAnime(Anime anime)
        {
            _db.Animes.Remove(anime);
            await _db.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Anime>> GetAllAnimes()
        {
           return await _db.Animes.ToListAsync();
        }

        public async Task<Anime?> GetAnimeByID(int id)
        {
            return await _db.Animes.FindAsync(id);
        }

        public async Task<IEnumerable<Anime>> SearchAnime(int? id = null, string? nome = null, string? diretor = null)
        {
            var query = _db.Animes.AsQueryable();

            if (id.HasValue)
                query = query.Where(q => q.Id == id.Value);

            if (!string.IsNullOrWhiteSpace(nome))
            {
                var nomeLower = nome.ToLower();
                query = query.Where(q => q.Name.ToLower().Contains(nomeLower));
            }

            if (!string.IsNullOrWhiteSpace(diretor))
            {
                var diretorLower = diretor.ToLower();
                query = query.Where(q => q.Diretor.ToLower().Contains(diretorLower));
            }

            return await query.ToListAsync();
        }

        public async Task UpdateAnime(Anime anime)
        {
            _db.Animes.Update(anime);
            await _db.SaveChangesAsync();
        }
    }
}
