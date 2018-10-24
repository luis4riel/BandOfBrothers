using NFe.Dominio.Base;
using NFe.Dominio.Exceptions;

namespace NFe.Dominio.Features.Destinatarios
{
    public class Destinatario : Contribuinte
    {

        public override void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome) && string.IsNullOrWhiteSpace(RazaoSocial))
                throw new DestinatarioEmptyRazaoNomeException();
            if (string.IsNullOrWhiteSpace(InscricaoEstadual))
                throw new DestinatarioEmptyInscricaoEstadualException();
            if (string.IsNullOrEmpty(Cpf.ValorFormatado) && string.IsNullOrEmpty(Cnpj.ValorFormatado))
                throw new DestinatarioEmptyCpfCnpjException();
            if (!string.IsNullOrEmpty(Cpf.ValorFormatado) && string.IsNullOrEmpty(Cnpj.ValorFormatado))
                if (Cpf.EhValido == false)
                    throw new CpfInvalidoException();
            if (!string.IsNullOrEmpty(Cnpj.ValorFormatado) && string.IsNullOrEmpty(Cpf.ValorFormatado))
                if (Cnpj.EhValido == false)
                    throw new CnpjInvalidoException();
        }   
    }
}
