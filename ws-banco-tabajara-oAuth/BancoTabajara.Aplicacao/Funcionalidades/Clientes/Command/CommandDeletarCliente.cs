using FluentValidation;
using FluentValidation.Results;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes.Command
{
    public class CommandDeletarCliente
    {
        public int Id { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<CommandDeletarCliente>
        {
            public Validator()
            {
                RuleFor(c => c.Id)
                      .NotNull().NotEmpty().WithMessage("O id não pode ser vazio").GreaterThan(0).WithMessage("O id deve ser maior que 0");
            }
        }
    }
}
