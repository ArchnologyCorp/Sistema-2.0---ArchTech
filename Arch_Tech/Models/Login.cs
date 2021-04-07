using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arch_Tech.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Insira um nome de usuário")]
        public string nome_usuario{ get; set; }
        [Required(ErrorMessage = "Insira uma senha")]
        public string senha { get; set; }
        public int tipo { get; set; }
    }
}