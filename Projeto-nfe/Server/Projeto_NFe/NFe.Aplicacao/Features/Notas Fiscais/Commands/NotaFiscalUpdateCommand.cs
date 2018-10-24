using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Aplicacao.Features.Notas_Fiscais.Commands
{
    [ExcludeFromCodeCoverage]
    public class NotaFiscalUpdateCommand
    {
        public virtual int Id { get; set; }
        public virtual string NaturezaOperacao { get; set; }
        public virtual DateTime? DataEmissao { get; set; }
        public virtual DateTime DataEntrada { get; set; }
        public virtual string ChaveAcesso { get; set; }
        public virtual bool Emitido { get; set; }
        public virtual IList<int> ProdutosId { get; set; }
        public virtual int DestinatarioId { get; set; }
        public virtual int EmitenteId { get; set; }
        public virtual int TransportadorId { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<NotaFiscalUpdateCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).GreaterThan(0);
                RuleFor(c => c.NaturezaOperacao).NotNull().NotEmpty();
                RuleFor(c => c.DataEmissao).NotNull().NotEmpty();
                RuleFor(c => c.DataEntrada).NotNull().NotEmpty();
                RuleFor(c => c.ChaveAcesso).NotNull().NotEmpty();
                RuleFor(c => c.Emitido).NotNull().NotEmpty();
                RuleFor(c => c.ProdutosId).NotNull();
                RuleFor(c => c.DestinatarioId).GreaterThan(0);
                RuleFor(c => c.EmitenteId).GreaterThan(0);
                RuleFor(c => c.TransportadorId).GreaterThan(0);
            }
        }
    }
}
