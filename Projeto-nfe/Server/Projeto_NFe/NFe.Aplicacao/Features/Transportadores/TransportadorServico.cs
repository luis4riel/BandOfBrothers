using AutoMapper;
using NFe.Aplicacao.Features.Transportadores.Commands;
using NFe.Aplicacao.Features.Transportadores.Queries;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Transportadores;
using System.Linq;

namespace NFe.Aplicacao.Features.Transportadores
{
    public class TransportadorServico : ITransportadorServico
    {
        ITransportadorRepositorio _repositorioTransportador;

        public TransportadorServico(ITransportadorRepositorio repositorio)
        {
            _repositorioTransportador = repositorio;
        }

        public int Adicionar(TransportadorRegisterCommand transportadorCmd)
        {
            var transportador = Mapper.Map<TransportadorRegisterCommand, Transportador>(transportadorCmd);
            var novoTransportador = _repositorioTransportador.Salvar(transportador);

            return (int)novoTransportador.Id;
        }

        public bool Atualizar(TransportadorUpdateCommand transportadorCmd)
        {
            var transportadorDb = _repositorioTransportador.PegarPorId(transportadorCmd.Id) ?? throw new NotFoundException();

            Mapper.Map<TransportadorUpdateCommand, Transportador>(transportadorCmd, transportadorDb);

            return _repositorioTransportador.Atualizar(transportadorDb);
        }

        public bool Deletar(TransportadorRemoveCommand transportadorCmd)
        {
            return _repositorioTransportador.Deletar(transportadorCmd.Id);
        }

        public TransportadorQuery PegarPorId(int id)
        {
            var transportador = _repositorioTransportador.PegarPorId(id);

            return Mapper.Map<Transportador, TransportadorQuery>(transportador);
        }

        IQueryable<Transportador> ITransportadorServico.PegarTodos()
        {
            return _repositorioTransportador.PegarTodos();
        }
    }
}
