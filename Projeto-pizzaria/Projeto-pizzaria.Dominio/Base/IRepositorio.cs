﻿using System.Collections.Generic;

namespace Projeto_pizzaria.Dominio.Base
{
    public interface IRepositorio<T> where T : Entidade
    {
        T Salvar(T entidade);
        T Atualizar(T entidade);
        void Deletar(T entidade);
        IEnumerable<T> PegarTodos();
        T PegarPorId(long id);
    }
}
