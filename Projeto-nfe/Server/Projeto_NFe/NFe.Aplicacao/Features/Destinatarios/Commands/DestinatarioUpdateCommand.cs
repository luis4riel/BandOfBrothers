using FluentValidation;
using FluentValidation.Results;
using NFe.Dominio.Features.Enderecos;
using NFe.Infra.Features.Cnpj;
using NFe.Infra.Features.Cpf;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Aplicacao.Features.Destinatarios.Commands
{
    [ExcludeFromCodeCoverage]
    public class DestinatarioUpdateCommand
    {
        public virtual int Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual string RazaoSocial { get; set; }

        public virtual Cpf Cpf { get; set; }

        public virtual Cnpj Cnpj { get; set; }

        public virtual string InscricaoEstadual { get; set; }

        public virtual Localizacao Endereco { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<DestinatarioUpdateCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Nome).NotNull().NotEmpty();
                RuleFor(c => c.RazaoSocial).NotNull().NotEmpty();
                RuleFor(c => c.Cpf).NotNull().NotEmpty();
                RuleFor(c => c.Cnpj).NotNull().NotEmpty();
                RuleFor(c => c.InscricaoEstadual).NotNull().NotEmpty();
                RuleFor(c => c.Endereco.Bairro).NotNull().NotEmpty();
                RuleFor(c => c.Endereco.Estado).NotNull().NotEmpty();
                RuleFor(c => c.Endereco.Logradouro).NotNull().NotEmpty();
                RuleFor(c => c.Endereco.Municipio).NotNull().NotEmpty();
                RuleFor(c => c.Endereco.Numero).NotNull().NotEmpty();
            }
        }
    }
}
