using FluentValidation;
using FluentValidation.Results;
using System;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes.Command
{
    public class CommandAtualizarCliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<CommandAtualizarCliente>
        {
            public Validator()
            {
                RuleFor(c => c.Id)
                    .NotNull().NotEmpty().WithMessage("O id não pode ser vazio").GreaterThan(0).WithMessage("O id deve ser maior que 0");
                RuleFor(c => c.Nome)
                    .NotNull().NotEmpty().WithMessage("O nome não pode ser vazio");
                RuleFor(c => c.Cpf)
                    .NotNull().NotEmpty().WithMessage("O cpf não pode ser vazio");
                RuleFor(c => c.Rg)
                    .NotNull().NotEmpty().WithMessage("O rg não pode ser vazio");
                RuleFor(c => c.DataNascimento)
                    .NotNull().LessThan(DateTime.Now).WithMessage("A data de nascimento não pode ser maior que a data atual");
            }
        }
    }
}