using System;
using Alura.CoisasAFazer.Core.Commands;
using Alura.CoisasAFazer.Core.Models;
using Alura.CoisasAFazer.Infrastructure;
using Alura.CoisasAFazer.Services.Handlers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Microsoft.Extensions;

namespace Alura.CoisasAFazer.Tests
{
    public class CadastraTarefaHandlerExecute
    {
        [Fact]
        public void QuandoExceptionForLancadaResultadoIsSucessDeveSerFalse()
        {
            //Arrange
            var comando = new CadastraTarefa("Estudar XUnit", new Categoria("Estudo"), new DateTime(2020, 1, 1));

            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var mock = new Mock<IRepositorioTarefas>();

            mock.Setup(x => x.IncluirTarefas(It.IsAny<Tarefa[]>()))
                .Throws<Exception>();

            var repo = mock.Object;
            var handler = new CadastraTarefaHandler(repo, mockLogger.Object);

            //Act
            CommandResult resultado = handler.Execute(comando);

            //Assert
            Assert.Equal(false, resultado.IsSuccess);
        }


        [Fact]
        public void QuandoExceptionForLancadaDeveLogarAMensagem()
        { 
            //Arrange
            var comando = new CadastraTarefa("Estudar XUnit", new Categoria("Estudo"), new DateTime(2020, 1, 1));
            var excecaoEsperada = new Exception("Houve um erro na inclusão de tarefas");

            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var mock = new Mock<IRepositorioTarefas>();

            mock.Setup(x => x.IncluirTarefas(It.IsAny<Tarefa[]>()))
                .Throws(excecaoEsperada);

            var repo = mock.Object;
            var handler = new CadastraTarefaHandler(repo, mockLogger.Object);

            //Act
            CommandResult resultado = handler.Execute(comando);

            ////Assert
            mockLogger.Verify(x => x.Log(LogLevel.Error,
                                         It.IsAny<EventId>(),
                                         It.IsAny<object>(),
                                         It.IsAny<Exception>(),
                                         It.IsAny<Func<object, Exception, string>>()),
                                         Times.Once());
        }
    }
}
