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
    public class DeleteAnimeHandlerTests
    {

        [Fact]
        public async Task DeleteAnimeTestIsValid()
        {
            var anime = new Anime(1, "naruto", "akira", "Esferas do Dragao");



            var mockRepo = new Mock<IAnimeRepository>();
            mockRepo.Setup(r => r.GetAnimeByID(1)).ReturnsAsync(anime);

            var handler = new DeleteAnimeHandler(mockRepo.Object);

            var command = new DeleteAnimeCommand(1);

            await handler.Handle(command, CancellationToken.None);

            mockRepo.Verify(r => r.DeleteAnime(anime), Times.Once());
        }



        [Fact]
        public async Task DeleteAnimeTestInvalid()
        {

            var mockRepo = new Mock<IAnimeRepository>();
            mockRepo.Setup(r => r.GetAnimeByID(1)).ReturnsAsync((Anime?)null);


            var handler = new DeleteAnimeHandler(mockRepo.Object);

            var command = new DeleteAnimeCommand(1);

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });
            mockRepo.Verify(r => r.DeleteAnime(It.IsAny<Anime>()), Times.Never());
        }

    }
}
