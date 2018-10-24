using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BancoTabajara.WebApi.Models.Clientes.ViewModel
{
    public class ViewModelCliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}