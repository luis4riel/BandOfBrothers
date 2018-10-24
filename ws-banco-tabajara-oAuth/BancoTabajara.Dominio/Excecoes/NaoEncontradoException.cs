namespace BancoTabajara.Dominio.Excecoes
{
    public class NaoEncontradoException : ExcecaoDeNegocio
    {
        public NaoEncontradoException() : base(CodigoErros.NotFound, "Registro Não Encontrado") { }
    }
}
