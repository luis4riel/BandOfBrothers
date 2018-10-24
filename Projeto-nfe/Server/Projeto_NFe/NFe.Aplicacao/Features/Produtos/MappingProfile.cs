using AutoMapper;
using NFe.Aplicacao.Features.Produtos.Commands;
using NFe.Aplicacao.Features.Produtos.Queries;
using NFe.Dominio.Features.Produtos;

namespace NFe.Aplicacao.Features.Produtos
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProdutoRegisterCommand, Produto>();
            CreateMap<Produto, ProdutoQuery>();
            CreateMap<ProdutoUpdateCommand, Produto>();
        }
    }
}
