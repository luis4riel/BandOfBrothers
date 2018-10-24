using AutoMapper;
using NFe.Aplicacao.Features.Transportadores.Commands;
using NFe.Aplicacao.Features.Transportadores.Queries;
using NFe.Dominio.Features.Transportadores;

namespace NFe.Aplicacao.Features.Transportadores
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TransportadorRegisterCommand, Transportador>();
            CreateMap<Transportador, TransportadorQuery>();
            CreateMap<TransportadorUpdateCommand, Transportador>();
        }
    }
}
