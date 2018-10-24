using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes.Commands
{
    public class ClienteAtualizarCommand
    {

        public virtual int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        
        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }
    }
    class Validator : AbstractValidator<ClienteAtualizarCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Nome)
                .NotNull().NotEmpty();
            RuleFor(c => c.Cpf)
                .NotEmpty().NotNull();
            RuleFor(c => c.Rg)
                .NotEmpty().NotNull();
            RuleFor(c => c.DataNascimento)
                .NotNull().LessThan(DateTime.Now);
        }
    }
}
