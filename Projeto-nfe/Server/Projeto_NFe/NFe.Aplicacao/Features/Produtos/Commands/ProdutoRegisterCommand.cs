using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Aplicacao.Features.Produtos.Commands
{
    [ExcludeFromCodeCoverage]
    public class ProdutoRegisterCommand
    {
        public virtual int CodigoProduto { get; set; }
        public virtual string Descricao { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual int ValorProdutoId { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ProdutoRegisterCommand>
        {
            public Validator()
            {
                RuleFor(c => c.CodigoProduto).GreaterThan(0);
                RuleFor(c => c.Descricao).NotNull().NotEmpty();
                RuleFor(c => c.Quantidade).GreaterThan(0);
                RuleFor(c => c.ValorProdutoId).GreaterThan(0);
            }
        }
    }
}
