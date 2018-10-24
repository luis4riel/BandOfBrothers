using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Aplicacao.Features.Destinatarios.Commands
{
    [ExcludeFromCodeCoverage]
    public class DestinatarioRemoveCommand
    {
        public virtual int Id { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<DestinatarioRemoveCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotNull().GreaterThan(0);
            }
        }
    }
}
