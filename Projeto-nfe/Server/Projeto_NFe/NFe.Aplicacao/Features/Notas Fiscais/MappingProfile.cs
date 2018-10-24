using AutoMapper;
using NFe.Aplicacao.Features.Notas_Fiscais.Commands;
using NFe.Aplicacao.Features.Notas_Fiscais.Queries;
using NFe.Dominio.Features.Notas_Fiscais;

namespace NFe.Aplicacao.Features.Notas_Fiscais
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NotaFiscalRegisterCommand, NotaFiscal>();
            CreateMap<NotaFiscal, NotaFiscalQuery>()
                .ForMember(oq => oq.TransportadorId, mo => mo.MapFrom(o => o.Transportador.Id))
                .ForMember(oq => oq.DestinatarioId, mo => mo.MapFrom(o => o.Destinatario.Id))
                .ForMember(oq => oq.EmitenteId, mo => mo.MapFrom(o => o.Emitente.Id));
            CreateMap<NotaFiscalUpdateCommand, NotaFiscal>();
        }
    }
}
