using AutoMapper;
using NFe.Aplicacao.Features.Destinatarios.Commands;
using NFe.Aplicacao.Features.Destinatarios.Queries;
using NFe.Dominio.Features.Destinatarios;

namespace NFe.Aplicacao.Features.Destinatarios
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DestinatarioRegisterCommand, Destinatario>();
            CreateMap<Destinatario, DestinatarioQuery>();
            CreateMap<DestinatarioUpdateCommand, Destinatario>();
        }
    }
}
