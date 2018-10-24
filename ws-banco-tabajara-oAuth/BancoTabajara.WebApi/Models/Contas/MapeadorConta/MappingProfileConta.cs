using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Command;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using BancoTabajara.WebApi.Models.Contas.ViewModel;

namespace BancoTabajara.WebApi.Models.Contas.MapeadorConta
{
    public class MappingProfileConta : Profile
    {
        public MappingProfileConta()
        {
            CreateMap<Conta, ViewModelConta>();
            CreateMap<CommandAtualizarConta, Conta>();
            CreateMap<CommandDeletarConta, Conta>();
            CreateMap<CommandRegistrarConta, Conta>();
        }
    }
}