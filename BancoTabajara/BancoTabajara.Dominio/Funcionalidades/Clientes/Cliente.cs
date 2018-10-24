using BancoTabajara.Dominio.Base;
using BancoTabajara.Dominio.Funcionalidades.Clientes.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Dominio.Funcionalidades.Clientes
{
    public class Cliente : Entidade
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }

        public override void Validar()
        {
            if (String.IsNullOrEmpty(Nome))
                throw new ClienteNomeEmBrancoException();
            if (String.IsNullOrEmpty(Cpf))
                throw new ClienteCpfEmBrancoException();
            if (String.IsNullOrEmpty(Rg))
                throw new ClienteRgEmBrancoException();
            if (DataNascimento > DateTime.Now)
                throw new ClienteDataNascimentoMaiorQueAgoraException();
        }
    }
}
