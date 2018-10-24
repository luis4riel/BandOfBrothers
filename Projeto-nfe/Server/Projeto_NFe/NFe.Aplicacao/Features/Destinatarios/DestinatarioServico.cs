using AutoMapper;
using NFe.Aplicacao.Features.Destinatarios.Commands;
using NFe.Aplicacao.Features.Destinatarios.Queries;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Destinatarios;
using NFe.Dominio.Features.Enderecos;
using System.Linq;

namespace NFe.Aplicacao.Features.Destinatarios
{
    public class DestinatarioServico : IDestinatarioServico
    {
        IDestinatarioRepositorio _repositorioDestinatario;

        public DestinatarioServico(IDestinatarioRepositorio repositorioDestinatario)
        {
            _repositorioDestinatario = repositorioDestinatario;
        }

        public int Adicionar(DestinatarioRegisterCommand destinatarioCmd)
        {
             var destinatario = Mapper.Map<DestinatarioRegisterCommand, Destinatario>(destinatarioCmd);
            var novoDestinatario = _repositorioDestinatario.Salvar(destinatario);

            return (int)novoDestinatario.Id;
        }
      
        public bool Atualizar(DestinatarioUpdateCommand destinatarioCmd)
        {
            var destinatarioDb = _repositorioDestinatario.PegarPorId(destinatarioCmd.Id) ?? throw new NotFoundException();

            Mapper.Map<DestinatarioUpdateCommand, Destinatario>(destinatarioCmd, destinatarioDb);

            return  _repositorioDestinatario.Atualizar(destinatarioDb);
        }

        public bool Deletar(DestinatarioRemoveCommand destinatarioCmd)
        {
            return _repositorioDestinatario.Deletar(destinatarioCmd.Id);
        }

        public DestinatarioQuery PegarPorId(int id)
        {
            var destinatario = _repositorioDestinatario.PegarPorId(id);

            return Mapper.Map<Destinatario, DestinatarioQuery>(destinatario);
        }

        IQueryable<Destinatario> IDestinatarioServico.PegarTodos()
        {
            return _repositorioDestinatario.PegarTodos();
        }
    }
}
