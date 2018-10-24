using NFe.Dominio.Base;

namespace NFe.Dominio.Features.Enderecos
{
    public class Localizacao
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
}
