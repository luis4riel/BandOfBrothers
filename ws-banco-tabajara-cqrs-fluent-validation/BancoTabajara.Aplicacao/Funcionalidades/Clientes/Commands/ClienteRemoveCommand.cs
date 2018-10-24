using FluentValidation;
using FluentValidation.Results;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes.Commands
{
    public class ClienteRemoveCommand
    {
        public int Id { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ClienteRemoveCommand>
        {
            public Validator()
            {
                RuleFor(p => p.Id).NotNull().GreaterThan(0);
            }
        }
    }
}
