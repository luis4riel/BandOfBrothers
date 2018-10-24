using AutoMapper;
using NFe.Aplicacao.Features.Emitentes.Commands;
using NFe.Aplicacao.Features.Emitentes.Queries;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Emitentes;
using NFe.Dominio.Features.Enderecos;
using System.Linq;

namespace NFe.Aplicacao.Features.Emitentes
{
    public class EmitenteServico : IEmitenteServico
    {
        IEmitenteRepositorio _repositorioEmitente;

        public EmitenteServico(IEmitenteRepositorio repositorioEmitente)
        {
            _repositorioEmitente = repositorioEmitente;
        }

        public int Adicionar(EmitenteRegisterCommand emitenteCmd)
        {
            var emitente = Mapper.Map<EmitenteRegisterCommand, Emitente>(emitenteCmd);
            var novoEmitente = _repositorioEmitente.Salvar(emitente);

            return (int)novoEmitente.Id;
        }

        public bool Atualizar(EmitenteUpdateCommand emitenteCmd)
        {
            var emitenteDb = _repositorioEmitente.PegarPorId(emitenteCmd.Id) ?? throw new NotFoundException();

            Mapper.Map<EmitenteUpdateCommand, Emitente>(emitenteCmd, emitenteDb);

            return _repositorioEmitente.Atualizar(emitenteDb);
        }

        public bool Deletar(EmitenteRemoveCommand emitenteCmd)
        {
            return _repositorioEmitente.Deletar(emitenteCmd.Id);
        }

        public EmitenteQuery PegarPorId(int id)
        {
            var emitente = _repositorioEmitente.PegarPorId(id);

            return Mapper.Map<Emitente, EmitenteQuery>(emitente);
        }

        IQueryable<Emitente> IEmitenteServico.PegarTodos()
        {
            return _repositorioEmitente.PegarTodos();
        }
    }
}
