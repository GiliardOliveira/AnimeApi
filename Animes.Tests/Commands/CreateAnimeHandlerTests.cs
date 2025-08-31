using Animes.Application.Commands;
using Animes.Application.Handlers;
using Animes.Domain.Entities;
using Animes.Domain.Interface;
using Moq;

namespace Animes.Tests.Commands
{
    public class CreateAnimeHandlerTests
    {
        [Fact]
        public async Task CreateAnimeTestIsValid()
        {

            var mockRepo = new Mock<IAnimeRepository>();
            var handler = new CreateAnimeHandler(mockRepo.Object);


            var command = new CreateAnimeCommand(
                "Atack on Titan",
                "Hajime Isayama",
                "Humanidade vs. Titãs: a busca pela liberdade."
            );

            await handler.Handle(command,CancellationToken.None);


            mockRepo.Verify(
                r => r.AddAnime(It.Is<Anime>(
                  a=> a.Name == "Atack on Titan"  &&
                  a.Diretor == "Hajime Isayama" &&
                  a.Resume == "Humanidade vs. Titãs: a busca pela liberdade.")),
                Times.Once
                
             );
                    
        }

        [Fact]
        public async Task CreateAnimeTestInvalid()
        {

            var mockRepo = new Mock<IAnimeRepository>();
            var handler = new CreateAnimeHandler(mockRepo.Object);


            var command = new CreateAnimeCommand(
                "",
                "Bones",
                "Irmãos buscam Pedra para restaurar corpos após alquimia fracassada. "
            );

            await Assert.ThrowsAsync<System.ArgumentException>(async () =>
            {
                await handler.Handle(command, CancellationToken.None);
            });

            mockRepo.Verify(r => r.AddAnime(It.IsAny<Anime>()), Times.Never);

        }
    }
}