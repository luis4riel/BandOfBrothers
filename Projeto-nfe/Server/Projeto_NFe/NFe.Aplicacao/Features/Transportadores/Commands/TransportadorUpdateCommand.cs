using FluentValidation;
using FluentValidation.Results;
using NFe.Infra.Features.Cnpj;
using NFe.Infra.Features.Cpf;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Aplicacao.Features.Transportadores.Commands
{
    [ExcludeFromCodeCoverage]
    public class TransportadorUpdateCommand
    {
        public virtual int Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual string RazaoSocial { get; set; }

        public virtual Cpf Cpf { get; set; }

        public virtual Cnpj Cnpj { get; set; }

        public virtual string InscricaoEstadual { get; set; }

        public virtual int EnderecoId { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<TransportadorUpdateCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).GreaterThan(0);
                RuleFor(c => c.Nome).NotNull().NotEmpty();
                RuleFor(c => c.RazaoSocial).NotNull().NotEmpty();
                RuleFor(c => c.Cpf).NotNull().NotEmpty();
                RuleFor(c => c.Cnpj).NotNull().NotEmpty();
                RuleFor(c => c.InscricaoEstadual).NotNull().NotEmpty();
                RuleFor(c => c.EnderecoId).GreaterThan(0);
            }
        }
    }
}
