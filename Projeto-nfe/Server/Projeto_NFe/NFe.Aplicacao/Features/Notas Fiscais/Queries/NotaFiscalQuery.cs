using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFe.Aplicacao.Features.Notas_Fiscais.Queries
{
    public class NotaFiscalQuery
    {
        public virtual int Id { get; set; }
        public virtual string NaturezaOperacao { get; set; }
        public virtual DateTime? DataEmissao { get; set; }
        public virtual DateTime DataEntrada { get; set; }
        public virtual string ChaveAcesso { get; set; }
        public virtual bool Emitido { get; set; }
        public virtual IList<int> ProdutosId { get; set; }
        public virtual int DestinatarioId { get; set; }
        public virtual int EmitenteId { get; set; }
        public virtual int TransportadorId { get; set; }
    }
}
