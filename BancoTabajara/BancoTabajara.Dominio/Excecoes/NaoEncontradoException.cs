namespace BancoTabajara.Dominio.Excecoes
{
    public class NaoEncontradoException : ExcecaoDeNegocio
    {
        public NaoEncontradoException() : base(CodigoErros.NotFound, "Registry not found") { }
    }
}
