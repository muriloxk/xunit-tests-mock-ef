using Alura.CoisasAFazer.Core.Commands;
using Alura.CoisasAFazer.Infrastructure;
using Alura.CoisasAFazer.Services.Handlers;
using Moq;
using Xunit;

namespace Alura.CoisasAFazer.Tests
{
    public class ObtemCategoriaPorIdExecute
    {
        [Fact]
        public void QuandoForExistenteDeveChamarObtemCategoriaPorIdUmaUnicaVez()
        {
            var mock = new Mock<IRepositorioTarefas>();
            var repo = mock.Object;

            //Arrange
            var comando = new ObtemCategoriaPorId(20);
            var handler = new ObtemCategoriaPorIdHandler(repo);

            //Act
            handler.Execute(comando);

            //Assert
            mock.Verify(r => r.ObtemCategoriaPorId(20), Times.Once);
        }
    }
}
