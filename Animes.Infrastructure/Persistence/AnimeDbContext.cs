using Animes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Animes.Infraestructure.Persistence
{
    public class AnimeDbContext: DbContext
    {
        public AnimeDbContext(DbContextOptions<AnimeDbContext> options) : base(options) { }


        public DbSet<Anime> Animes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anime>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e=>e.Name).IsRequired();
                entity.Property(e => e.Diretor);
                entity.Property(e => e.Resume);
            });

        }
    }
}
