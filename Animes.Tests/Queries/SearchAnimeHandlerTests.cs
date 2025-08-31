using Animes.Application.Handlers;
using Animes.Application.Queries;
using Animes.Domain.Entities;
using Animes.Domain.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Tests.Queries
{
    
    public class SearchAnimeHandlerTests
    {
        [Fact]
        public async Task SearchAnimeIsValid()
        {

            var mockRepo = new Mock<IAnimeRepository>();

            var fakeAnimes = new List<Anime>
            {
                new Anime { Id = 1, Name = "Naruto", Diretor = "Masashi Kishimoto", Resume = "Ninja da Vila da Folha" },
                new Anime { Id = 2, Name = "One Piece", Diretor = "Eiichiro Oda", Resume = "Aventura dos Piratas" }
            };

            mockRepo.Setup(r => r.SearchAnime(null,"Naruto", null)).ReturnsAsync(fakeAnimes.Where(a => a.Name.Contains("Naruto")));


            var handler = new SearchAnimeHandler(mockRepo.Object);

            var query = new SearchAnimeQuery(null, "Naruto", null);

            var result = await handler.Handle(query, CancellationToken.None);


            Assert.Single(result);

            Assert.Equal("Naruto", result[0].Nome);


            mockRepo.Verify(r => r.SearchAnime(null, "Naruto", null), Times.Once);

        }

        [Fact]
        public async Task SearchAnimeInvalid()
        {
            var mockRepo = new Mock<IAnimeRepository>();

            mockRepo.Setup(r => r.SearchAnime(null, "Bleach", null))
                    .ReturnsAsync(new List<Anime>());

            var handler = new SearchAnimeHandler(mockRepo.Object);
            var query = new SearchAnimeQuery(null, "Bleach", null);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Empty(result);
            mockRepo.Verify(r => r.SearchAnime(null, "Bleach", null), Times.Once);
        }
    }
}
