using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Command;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using BancoTabajara.WebApi.Models.Clientes.ViewModel;

namespace BancoTabajara.WebApi.Models.Clientes.MapeadorCliente
{
    public class MappingProfileCliente : Profile
    {
        public MappingProfileCliente()
        {
            CreateMap<CommandRegistrarCliente, Cliente>();
            CreateMap<Cliente, ViewModelCliente>();
            CreateMap<CommandAtualizarCliente, Cliente>();
            CreateMap<CommandDeletarCliente, Cliente>();
        }
    }
}
