using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Commands;
using BancoTabajara.Aplicacao.Funcionalidades.Clientes.Queries;
using BancoTabajara.Dominio.Funcionalidades.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Clientes
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteRegistroCommand, Cliente>();
            CreateMap<Cliente, ClienteQuery>();
            CreateMap<ClienteAtualizarCommand, Cliente>();
        }
    }
}
