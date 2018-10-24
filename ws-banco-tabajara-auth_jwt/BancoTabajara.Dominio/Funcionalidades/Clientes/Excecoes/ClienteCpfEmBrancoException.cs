using BancoTabajara.Dominio.Excecoes;

namespace BancoTabajara.Dominio.Funcionalidades.Clientes.Excecoes
{
    public class ClienteCpfEmBrancoException : ExcecaoDeNegocio
    {
        public ClienteCpfEmBrancoException() : base("Cpf não pode estar em branco")
        {
        }
    }
}
