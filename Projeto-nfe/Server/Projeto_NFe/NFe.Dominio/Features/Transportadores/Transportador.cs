using NFe.Dominio.Base;
using NFe.Dominio.Exceptions;

namespace NFe.Dominio.Features.Transportadores
{
    public enum Frete
    {
        EMITENTE = 0, DESTINATARIO
    }

    public class Transportador : Contribuinte
    {
        public Frete ResponsabilidadeFrete { get; set; }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(Nome) && string.IsNullOrEmpty(RazaoSocial))
                throw new TransportadorEmptyNomeRazaoException();
            if (string.IsNullOrEmpty(InscricaoEstadual))
                throw new TransportadorEmptyInscricaoEstadualException();
            if (string.IsNullOrEmpty(Cpf.ValorFormatado) && string.IsNullOrEmpty(Cnpj.ValorFormatado))
                throw new TransportadorEmptyCpfCnpjException();
            if (!string.IsNullOrEmpty(Cpf.ValorFormatado) && string.IsNullOrEmpty(Cnpj.ValorFormatado))
                if (Cpf.EhValido == false)
                    throw new CpfInvalidoException();
            if (!string.IsNullOrEmpty(Cnpj.ValorFormatado) && string.IsNullOrEmpty(Cpf.ValorFormatado))
                if (Cnpj.EhValido == false)
                    throw new CnpjInvalidoException();
        }
    }
}
