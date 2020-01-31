﻿using System;
using System.Collections.Generic;
using System.Linq;
using Alura.CoisasAFazer.Core.Models;
using Alura.CoisasAFazer.Infrastructure;

namespace Alura.CoisasAFazer.Tests
{
    public class FakeRepositoryTarefas : IRepositorioTarefas
    {
        public List<Tarefa> Tarefas { get; private set; }

        public FakeRepositoryTarefas()
        {
            Tarefas = new List<Tarefa>();
        }

        public void IncluirTarefas(params Tarefa[] tarefas)
        {
            Tarefas.AddRange(tarefas);
        }

        public void AtualizarTarefas(params Tarefa[] tarefas)
        {
            throw new NotImplementedException();
        }

        public void ExcluirTarefas(params Tarefa[] tarefas)
        {
            throw new NotImplementedException();
        }

        public Categoria ObtemCategoriaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tarefa> ObtemTarefas(Func<Tarefa, bool> filtro)
        {
            return Tarefas.Where(filtro).ToList();
        }
    }
}
