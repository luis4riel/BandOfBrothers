using AutoMapper;
using NFe.Aplicacao.Features.Notas_Fiscais.Commands;
using NFe.Aplicacao.Features.Notas_Fiscais.Queries;
using NFe.Dominio.Exceptions;
using NFe.Dominio.Features.Destinatarios;
using NFe.Dominio.Features.Emitentes;
using NFe.Dominio.Features.Notas_Fiscais;
using NFe.Dominio.Features.Transportadores;
using NFe.Infra.PDF.Features.Notas_Fiscais;
using NFe.Infra.XML.Features.NotasFiscais;
using System.Linq;

namespace NFe.Aplicacao.Features.Notas_Fiscais
{
    public class NotaFiscalServico : INotaFiscalServico
    {
        NotaXmlRepositorio notaXml;
        INotaFiscalRepositorio _notaFiscalRepositorio;
        IEmitenteRepositorio _emitenteRepositorio;
        IDestinatarioRepositorio _destinatarioRepositorio;
        ITransportadorRepositorio _transportadorRepositorio;

        public NotaFiscalServico(INotaFiscalRepositorio notaFiscalRepositorio, IEmitenteRepositorio emitenteRepositorio, ITransportadorRepositorio transportadorRepositorio, IDestinatarioRepositorio destinatarioRepositorio )
        {
            notaXml = new NotaXmlRepositorio();
            _notaFiscalRepositorio = notaFiscalRepositorio;
            _emitenteRepositorio = emitenteRepositorio;
            _destinatarioRepositorio = destinatarioRepositorio;
            _transportadorRepositorio = transportadorRepositorio;
        }

        public void ExportaXml(string caminho, NotaFiscal notaFiscal)
        {
            NotaXmlRepositorio NotaXml = new NotaXmlRepositorio();

            if (string.IsNullOrEmpty(caminho))
                throw new PathNullOrNotFound();
            else
                NotaXml.ExportaParaArquivoXml(caminho, notaFiscal);
        }

        public void ExportarPdf(string caminho, NotaFiscal notaFiscal)
        {
            if (string.IsNullOrEmpty(caminho))
                throw new PathNullOrNotFound();
            else
                NotaFiscalPdf.Exportar(caminho, notaFiscal);
        }

        public int Adicionar(NotaFiscalRegisterCommand notaFiscalCmd)
        {
            var notaFiscal = Mapper.Map<NotaFiscalRegisterCommand, NotaFiscal>(notaFiscalCmd);
            notaFiscal.Emitente = _emitenteRepositorio.PegarPorId(notaFiscalCmd.EmitenteId) ?? throw new NotFoundException();
            notaFiscal.Transportador = _transportadorRepositorio.PegarPorId(notaFiscalCmd.TransportadorId) ?? throw new NotFoundException();
            notaFiscal.Destinatario = _destinatarioRepositorio.PegarPorId(notaFiscalCmd.DestinatarioId) ?? throw new NotFoundException();

            var novaNota = _notaFiscalRepositorio.Salvar(notaFiscal);

            return (int)novaNota.Id;
        }

        public bool Atualizar(NotaFiscalUpdateCommand notaFiscalCmd)
        {
            var notaFiscalDb = _notaFiscalRepositorio.PegarPorId(notaFiscalCmd.Id) ?? throw new NotFoundException();
            var emitente = _emitenteRepositorio.PegarPorId(notaFiscalCmd.EmitenteId) ?? throw new NotFoundException();
            var transportador = _transportadorRepositorio.PegarPorId(notaFiscalCmd.TransportadorId) ?? throw new NotFoundException();
            var destinatario = _destinatarioRepositorio.PegarPorId(notaFiscalCmd.DestinatarioId) ?? throw new NotFoundException();
                       
            Mapper.Map<NotaFiscalUpdateCommand, NotaFiscal>(notaFiscalCmd, notaFiscalDb);
            notaFiscalDb.Emitente = emitente;
            notaFiscalDb.Transportador = transportador;
            notaFiscalDb.Destinatario = destinatario;

            return _notaFiscalRepositorio.Atualizar(notaFiscalDb);
        }

        public NotaFiscalQuery PegarPorId(int id)
        {
            var notaFiscal = _notaFiscalRepositorio.PegarPorId(id);

            return Mapper.Map<NotaFiscal, NotaFiscalQuery>(notaFiscal);
        }

        public IQueryable<NotaFiscal> PegarTodos()
        {
            return _notaFiscalRepositorio.PegarTodos();
        }

        public bool Deletar(NotaFiscalRemoveCommand notaFiscalCmd)
        {
            return _notaFiscalRepositorio.Deletar(notaFiscalCmd.Id);
        }
    }
}
