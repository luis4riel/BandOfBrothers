
using FluentValidation;
using FluentValidation.Results;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas.Commands
{
    public class ContaRemoveCommand
    {
        public virtual int Id { get; set; }
        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ContaRemoveCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).NotNull().GreaterThan(0);
            }
        }
    }
}
