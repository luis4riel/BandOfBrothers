﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Projeto_pizzaria.Dominio.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class PathNullOrNotFound : Exception
    {
        public PathNullOrNotFound() : base("O caminho está nulo ou não foi encontrado")
        {

        }
    }
}
