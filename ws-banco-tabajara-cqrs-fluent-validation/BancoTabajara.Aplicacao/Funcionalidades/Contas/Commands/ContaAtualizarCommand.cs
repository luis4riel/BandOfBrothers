using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas.Commands
{
    public class ContaAtualizarCommand
    {
        public virtual int Id { get; set; }
        public virtual bool Estado { get; set; }
        public decimal Limite { get; set; }
        public virtual int ClienteId { get; set; }
        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }
        class Validator : AbstractValidator<ContaAtualizarCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Limite).GreaterThan(0);
                RuleFor(c => c.ClienteId).GreaterThan(0);
            }
        }
    }
   
}
