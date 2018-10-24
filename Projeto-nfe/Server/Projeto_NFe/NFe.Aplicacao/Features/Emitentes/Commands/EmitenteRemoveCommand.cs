﻿using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Aplicacao.Features.Emitentes.Commands
{
    [ExcludeFromCodeCoverage]
    public class EmitenteRemoveCommand
    {
        public virtual int Id { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<EmitenteRemoveCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotNull().GreaterThan(0);
            }
        }
    }
}
