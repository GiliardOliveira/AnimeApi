using Animes.Application.Commands;
using Animes.Application.Handlers;
using Animes.Domain.Entities;
using Animes.Domain.Interface;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animes.Tests.Commands
{
    public class UpdateAnimeHandlerTests
    {
        [Fact]
        public async Task UpdateAnimeTestIsValid()
        {

            var anime = new Anime(1, "naruto", "akira", "Esferas do Dragao");



            var mockRepo = new Mock<IAnimeRepository>();
            mockRepo.Setup(r => r.GetAnimeByID(1)).ReturnsAsync(anime);

            var handler = new UpdateAnimeHandler(mockRepo.Object);

            var command = new UpdateAnimeCommand(1, "Naruto", "Masashi Kishimoto", "O Garoto da Profecia - Jiraya");

            await handler.Handle(command, CancellationToken.None);


            Assert.Equal("Naruto", anime.Name);
            Assert.Equal("Masashi Kishimoto", anime.Diretor);
            Assert.Equal("O Garoto da Profecia - Jiraya", anime.Resume);



            mockRepo.Verify(r => r.UpdateAnime(anime), Times.Once());

        }

        [Fact]
        public async Task UpdateAnimeTestInvalid()
        {
            var mockRepo = new Mock<IAnimeRepository>();

            mockRepo.Setup(r => r.GetAnimeByID(1)).ReturnsAsync((Anime?)null);

            var handler = new UpdateAnimeHandler(mockRepo.Object);

            var command = new UpdateAnimeCommand(1, "Any Name", "Any Diretor", "Any Resume");

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            mockRepo.Verify(r => r.UpdateAnime(It.IsAny<Anime>()), Times.Never);
        }
    }
}
