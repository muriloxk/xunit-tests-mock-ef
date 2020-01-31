using System;
using System.Linq;
using Alura.CoisasAFazer.Core.Commands;
using Alura.CoisasAFazer.Core.Models;
using Alura.CoisasAFazer.Services.Handlers;
using Xunit;

namespace Alura.CoisasAFazer.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var comando = new CadastraTarefa("Estudar xUnit", new Categoria("Estudo"), new DateTime(2019, 12, 10));


            var repoFakeTarefas = new FakeRepositoryTarefas();
            //Vamos passar o repositório por injeção de depedencia:
            var handler = new CadastraTarefaHandler(repoFakeTarefas);
            handler.Execute(comando);

            //Vamos fazer o teste do repositório por mock utilizando um repositorio fake.
            var tarefa = repoFakeTarefas.ObtemTarefas(t => t.Titulo == "Estudar xUnit").FirstOrDefault();

            Assert.NotNull(tarefa);
        }
    }
}
