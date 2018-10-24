using Projeto_pizzaria.Dominio.Base;

namespace Projeto_pizzaria.Dominio.Features.Enderecos
{
    public class Localizacao
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
		public string Complemento { get; set; }
		public string Cep { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Logradouro))
                throw new EnderecoEmptyLogradouroException();
            if (string.IsNullOrEmpty(Bairro))
                throw new EnderecoEmptyBairroException();
			if (string.IsNullOrEmpty( Complemento ))
				throw new EnderecoEmptyComplementoException();
			if (string.IsNullOrEmpty(Numero))
                Numero = "s/n";
        }
    }
}
