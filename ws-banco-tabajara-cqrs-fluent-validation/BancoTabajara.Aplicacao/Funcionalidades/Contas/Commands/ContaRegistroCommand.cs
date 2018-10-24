using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas.Commands
{
    [ExcludeFromCodeCoverage]
    public class ContaRegistroCommand
    {
        public virtual string NumeroConta { get; set; }
        public virtual bool Estado { get; set; }
        public decimal Limite { get; set; }
        public virtual int ClienteId { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }
        private void GerarNumeroConta()
        {
            Random rdn = new Random();
            NumeroConta = rdn.Next(0, 999999).ToString("D6");
        }
    }
    class Validator : AbstractValidator<ContaRegistroCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Limite).GreaterThan(0);
            RuleFor(c => c.ClienteId).GreaterThan(0);
        }
    }
}
