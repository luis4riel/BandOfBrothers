﻿using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Aplicacao.Features.Produtos.Commands
{
    [ExcludeFromCodeCoverage]
    public class ProdutoRemoveCommand
    {
        public virtual int Id { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ProdutoRemoveCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotNull().GreaterThan(0);
            }
        }
    }
}
