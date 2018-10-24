using AutoMapper;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Commands;
using BancoTabajara.Aplicacao.Funcionalidades.Contas.Queries;
using BancoTabajara.Dominio.Funcionalidades.Contas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Aplicacao.Funcionalidades.Contas
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContaRegistroCommand, Conta>();
            CreateMap<Conta, ContaQuery>();
            CreateMap<ContaAtualizarCommand, Conta>();
        }
    }
}
