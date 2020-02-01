using System;
using System.Linq;
using Alura.CoisasAFazer.Core.Commands;
using Alura.CoisasAFazer.Core.Models;
using Alura.CoisasAFazer.Infrastructure;
using Alura.CoisasAFazer.Services.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Alura.CoisasAFazer.Tests
{
    public class UnitTest1
    {
        //Aqui vamos ter dois exemplos utilzando um context utilizando
        //InMemory do EF Core e um criando um FakeRepository.

        [Fact]
        public void Test1()
        {
            var comando = new CadastraTarefa("Estudar xUnit", new Categoria("Estudo"), new DateTime(2019, 12, 10));

            //var repoFakeTarefas = new FakeRepositoryTarefas();
            var options = new DbContextOptionsBuilder<DbTarefasContext>()
                                 .UseInMemoryDatabase("DbTarefasContext")
                                 .Options;

            var dbContext = new DbTarefasContext(options);

            var repoInMemory = new RepositorioTarefa(dbContext);
            var mockLogger = new Mock<ILogger<CadastraTarefaHandler>>();

            //var handler = new CadastraTarefaHandler(repoFakeTarefas);
            var handler = new CadastraTarefaHandler(repoInMemory, mockLogger.Object);
            handler.Execute(comando);

            //Vamos fazer o teste do repositório por mock utilizando um repositorio fake.
            //var tarefa = repoFakeTarefas.ObtemTarefas(t => t.Titulo == "Estudar xUnit").FirstOrDefault();
            var tarefa = repoInMemory.ObtemTarefas(x => x.Titulo == "Estudar xUnit").FirstOrDefault();
            Assert.NotNull(tarefa);
        }
    }
}
