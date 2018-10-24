using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics.CodeAnalysis;

namespace NFe.Aplicacao.Features.Produtos.Commands
{
    [ExcludeFromCodeCoverage]
    public class ProdutoUpdateCommand
    {
        public virtual int Id { get; set; }
        public virtual int CodigoProduto { get; set; }
        public virtual string Descricao { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual int ValorProdutoId { get; set; }

        public virtual ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ProdutoUpdateCommand>
        {
            public Validator()
            {
                RuleFor(c => c.Id).GreaterThan(0);
                RuleFor(c => c.CodigoProduto).GreaterThan(0);
                RuleFor(c => c.Descricao).NotNull().NotEmpty();
                RuleFor(c => c.Quantidade).GreaterThan(0);
                RuleFor(c => c.ValorProdutoId).GreaterThan(0);
            }
        }
    }
}
