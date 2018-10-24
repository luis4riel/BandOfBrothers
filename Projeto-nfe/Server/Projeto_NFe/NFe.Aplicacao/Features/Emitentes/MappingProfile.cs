using AutoMapper;
using NFe.Aplicacao.Features.Emitentes.Commands;
using NFe.Aplicacao.Features.Emitentes.Queries;
using NFe.Dominio.Features.Emitentes;

namespace NFe.Aplicacao.Features.Emitentes
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmitenteRegisterCommand, Emitente>();
            CreateMap<Emitente, EmitenteQuery>();
            CreateMap<EmitenteUpdateCommand, Emitente>();
        }
    }
}
