using NFe.Infra.Features.Cnpj;
using NFe.Infra.Features.Cpf;

namespace NFe.Aplicacao.Features.Emitentes.Queries
{
    public class EmitenteQuery
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string RazaoSocial { get; set; }
        public virtual Cpf Cpf { get; set; }
        public virtual Cnpj Cnpj { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual int EnderecoId { get; set; }
    }
}
