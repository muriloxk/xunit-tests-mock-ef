using System;
using Alura.CoisasAFazer.Core.Commands;
using Alura.CoisasAFazer.Core.Models;
using Alura.CoisasAFazer.Infrastructure;
using Alura.CoisasAFazer.Services.Handlers;
using Moq;
using Xunit;

namespace Alura.CoisasAFazer.Tests
{
    public class CadastraTarefaHandlerExecute
    {
        [Fact]
        public void QuandoExceptionForLancadaResultadoIsSucessDeveSerFalse()
        {
            //Arrange
            var comando = new CadastraTarefa("Estudar XUnit", new Categoria("Estudo"), new DateTime(2020, 1, 1));

            var mock = new Mock<IRepositorioTarefas>();

            mock.Setup(x => x.IncluirTarefas(It.IsAny<Tarefa[]>()))
                .Throws<Exception>();

                
            var repo = mock.Object;
            var handler = new CadastraTarefaHandler(repo);

            //Act
            CommandResult resultado = handler.Execute(comando);


            //Assert
            Assert.Equal(false, resultado.IsSuccess);
        }
    }
}
