using FluentValidation;
using FluentValidation.Results;
using System;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes.Commands
{
    public class ClienteRegistroCommand
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ClienteRegistroCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Nome)
                    .NotNull().NotEmpty();
                RuleFor(c => c.DataNascimento)
                    .NotNull().LessThan(DateTime.Now);
            }
        }
    }
}
