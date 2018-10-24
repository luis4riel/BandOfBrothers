namespace Projeto_pizzaria.Infra.Features.Cnpj
{
    public class Cnpj
    {
        private string Valor { get; set; }
		public string ValorFormatado { get; private set; }
		public bool EhValido;
        static readonly int[] Multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        static readonly int[] Multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        private Cnpj() { }

		public Cnpj(string value)
        {
            Valor = value;
            ValorFormatado = "";
            EhValido = false;

            if (string.IsNullOrEmpty(value))
                return;

            FormatarCnpj(value);
            ValidarCnpj(ValorFormatado);
        }

        private void FormatarCnpj(string value)
        {
            ValorFormatado = value.Trim();
            ValorFormatado = ValorFormatado.Replace(".", "").Replace("-", "").Replace("/", "");
        }

        private void ValidarCnpj(string value)
        {
            var digitosIdenticos = true;
            var ultimoDigito = -1;
            var posicao = 0;
            var totalDigito1 = 0;
            var totalDigito2 = 0;

            foreach (var c in Valor)
            {
                if (char.IsDigit(c))
                {
                    var digito = c - '0';
                    if (posicao != 0 && ultimoDigito != digito)
                    {
                        digitosIdenticos = false;
                    }

                    ultimoDigito = digito;
                    if (posicao < 12)
                    {
                        totalDigito1 += digito * Multiplicador1[posicao];
                        totalDigito2 += digito * Multiplicador2[posicao];
                    }
                    else if (posicao == 12)
                    {
                        var dv1 = (totalDigito1 % 11);
                        dv1 = dv1 < 2
                            ? 0
                            : 11 - dv1;

                        if (digito != dv1)
                        {
                            EhValido = false;
                            return;
                        }

                        totalDigito2 += dv1 * Multiplicador2[12];
                    }
                    else if (posicao == 13)
                    {
                        var dv2 = (totalDigito2 % 11);

                        dv2 = dv2 < 2
                            ? 0
                            : 11 - dv2;

                        if (digito != dv2)
                        {
                            EhValido = false;
                            return;
                        }
                    }

                    posicao++;
                }
            }

            EhValido = (posicao == 14) && !digitosIdenticos;
        }

        public static implicit operator Cnpj(string value)
            => new Cnpj(value);

        public static implicit operator string(Cnpj cnpj)
        => cnpj.Valor;
    }
}
