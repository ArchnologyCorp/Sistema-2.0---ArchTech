using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Arch_Tech.Models
{
    public class Pedido
    {
        /*atributos principais da classe*/
        [Display(Name = "Número do Pedido")]
        public int cod_pedido { get; set; }
        [Display(Name = "Valor Total")]
        [Required(ErrorMessage = "Insira o valor do pedido")]
        public double valor { get; set; }
        [Display(Name = "Data do Pedido")]
        [Required(ErrorMessage = "Insira a data do pedido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime data { get; set; }
        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Insira a situação do pedido")]
        public string situacao { get; set; }
        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Insira a quantidade do pedido")]
        public int quantidade_pedido { get; set; }

        /*atributos que recereberão dropdown*/
        [Display(Name = "Método de Pagamento")]
        [Required(ErrorMessage = "Insira um método de pagamento")]
        public int cod_pagamento { get; set; }
        [Display(Name = "Produto adicionado")]
        [Required(ErrorMessage = "Insira um produto")]
        public int cod_produto { get; set; }
        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Insira um cliente")]
        public int cod_cli { get; set; }


        /*atributos para consulta específica*/
        [Display(Name = "Tipo de Pagamento")]
        public string tipo_pagamento { get; set; }

        [Display(Name = "Descrição")]
        public string desc_produto { get; set; }

        [Display(Name = "Produto")]
        public string nome_produto { get; set; }  
        
        [Display(Name = "Cliente")]
        public string nome_cli { get; set; }


    }
}