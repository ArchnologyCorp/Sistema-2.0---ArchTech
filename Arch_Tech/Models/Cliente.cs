using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arch_Tech.Models
{
    public class Cliente
    {
        public int cod_cli { get; set; }
        public string nome_cli { get; set; }
        public string email_cli { get; set; }
        public string cpf_cli { get; set; }
        public string telefone_cli { get; set; }
    }
}