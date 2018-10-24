using BancoTabajara.Dominio.Excecoes;

namespace BancoTabajara.Dominio.Funcionalidades.Contas.Exececoes
{
    public class ContaInativaException : ExcecaoDeNegocio
    {
        public ContaInativaException() : base("Não é permitido realizar movimentações com a conta inativa")
        {
        }
    }
}
