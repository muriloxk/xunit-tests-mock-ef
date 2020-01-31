using System;
using System.Collections.Generic;
using Alura.CoisasAFazer.Core.Models;
using Alura.CoisasAFazer.Core.Commands;
using Alura.CoisasAFazer.Services.Handlers;
using Alura.CoisasAFazer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Linq;

namespace Alura.CoisasAFazer.Tests
{
    public class GerenciaPrazoDasTarefasHandlerExecute
    {
        [Fact]
        public void QuandoTarefasEstiveremAtrasadasDeveMudarSeuStatus()
        {
            //Arrage
            var categoriaSaude = new Categoria("Saude");
            var categoriaHigiene = new Categoria("Higiene");
            var categoriaTrabalho = new Categoria("Trabalho");

            List<Tarefa> tarefas = new List<Tarefa>()
            {
                new Tarefa(1, "Comprar remédio para dor de cabeça", categoriaSaude, new DateTime(2020,1,1), null, StatusTarefa.Criada),
                new Tarefa(2, "Marcar consulta com o médico", categoriaSaude, new DateTime(2020,1,1), null, StatusTarefa.Criada),
                new Tarefa(3, "Marcar reunião com o Marcelo", categoriaTrabalho, new DateTime(2020,1,1), null, StatusTarefa.Criada),
                new Tarefa(4, "Realizar faxina", categoriaHigiene, new DateTime(2020,1,1), null, StatusTarefa.Criada),
            };

            var optionsContext = new DbContextOptionsBuilder<DbTarefasContext>()
                                    .UseInMemoryDatabase<DbTarefasContext>("DbTarefasContext")
                                    .Options;

            var repositorioTarefas = new RepositorioTarefa(new DbTarefasContext(optionsContext));
            repositorioTarefas.IncluirTarefas(tarefas.ToArray());

            var comando = new GerenciaPrazoDasTarefas(new DateTime(2020, 2, 1));
            var GerenciaPrazoDasTarefasHandler = new GerenciaPrazoDasTarefasHandler(repositorioTarefas);

            //Act
            GerenciaPrazoDasTarefasHandler.Execute(comando);

            //Assert
            var tarefasEmAtraso = repositorioTarefas.ObtemTarefas(t => t.Status == StatusTarefa.EmAtraso);
            Assert.Equal(4, tarefasEmAtraso.Count());
        }
    }
}
