using System;
using Xunit;
using Alura.CoisasAFazer.WebApp.Controllers;
using Alura.CoisasAFazer.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using Alura.CoisasAFazer.Services.Handlers;
using Alura.CoisasAFazer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Alura.CoisasAFazer.Core.Models;

namespace Alura.CoisasAFazer.Tests
{
    public class TarefasControllerEndpointCadastraTarefa
    {
        [Fact]
        public void QuandoExcecaoForLancadaDeveRetornarStatusCode500()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();
            var mockRepo = new Mock<IRepositorioTarefas>();

            mockRepo.Setup(x => x.ObtemCategoriaPorId(It.IsAny<int>()))
                    .Returns(new Categoria(20, "Casa"));

            mockRepo.Setup(x => x.IncluirTarefas(It.IsAny<Tarefa[]>()))
                    .Throws(new Exception("Houve um erro ao incluir as tarefas"));

            var repo = mockRepo.Object;

            var controlador = new TarefasController(repo, mockLogger.Object);
            var model = new CadastraTarefaVM();
            model.IdCategoria = 20;
            model.Titulo = "Estudar Xunit";
            model.Prazo = new DateTime(2019, 12, 31);

            //Act
            var retorno = controlador.EndpointCadastraTarefa(model);

            //Assert
            Assert.IsType<StatusCodeResult>(retorno);
            var statusCode = (retorno as StatusCodeResult).StatusCode;
            Assert.Equal(500, statusCode);
        }
    }
}
