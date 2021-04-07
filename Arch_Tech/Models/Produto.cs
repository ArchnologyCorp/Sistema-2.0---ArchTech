using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arch_Tech.Models
{
    public class Produto
    {
        [Display(Name = "Código do Produto")]
        public int cod_produto { get; set; }
        [Display(Name = "Nome do Produto")]
        public string nome_produto{ get; set; }
        [Display(Name = "Descrição do Produto")]
        public string desc_produto{ get; set; }
        [Display(Name = "Quantidade do Produto")]
        public int quantidade_estoque{ get; set; }
        [Display(Name = "Valor Unitário")]
        public double valor_unitario{ get; set; }
        [Display(Name = "Fornecedor")]
        public int cod_fornecedor{ get; set; }
        [Display(Name = "Fornecedor")]
        public string marca { get; set; }
        [Display(Name = "Quantidade do Produto")]
        public string quantidade_estoque_lista { get; set;}
    }
}