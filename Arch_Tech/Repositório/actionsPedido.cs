using Arch_Tech.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arch_Tech.Repositório
{
    public class actionsPedido
    {
        conexao cn = new conexao();
        public void cadastraPedido(Pedido pedido)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastraPedido(@qtd, @valor, @data, @situacao,@cod_pagamento, @cod_cli, @cod_produto);", cn.conectarBD());
            cmd.Parameters.Add("@qtd", MySqlDbType.Int32).Value = pedido.quantidade_pedido;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = pedido.valor;
            cmd.Parameters.Add("@data", MySqlDbType.DateTime).Value = pedido.data.ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@situacao", MySqlDbType.VarChar).Value = pedido.situacao;
            cmd.Parameters.Add("@cod_pagamento", MySqlDbType.VarChar).Value = pedido.cod_pagamento;
            cmd.Parameters.Add("@cod_cli", MySqlDbType.Int32).Value = pedido.cod_cli;
            cmd.Parameters.Add("@cod_produto", MySqlDbType.Int32).Value = pedido.cod_produto;

            cmd.ExecuteNonQuery();

            cn.desconectarBD();

        }

        public void consultaBuscaPedido(Pedido pedido)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbPedido;", cn.conectarBD());
            cmd.Parameters.AddWithValue("@cod", MySqlDbType.Int32).Value = pedido.cod_pedido;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                pedido.cod_pedido = Convert.ToInt32(dr[0]);
                pedido.quantidade_pedido = Convert.ToInt32(dr[1]);
                pedido.valor = Convert.ToDouble(dr[2]);
                pedido.data = Convert.ToDateTime(dr[3].ToString());
                pedido.situacao = dr[4].ToString();
                pedido.cod_pagamento = Convert.ToInt32(dr[5]);
                pedido.cod_cli = Convert.ToInt32(dr[6]);
                pedido.cod_produto = Convert.ToInt32(dr[7]);
            }
        }

        public List<Pedido> Listar()
        {
            using (cn.conectarBD())
            {
                MySqlCommand cmd = new MySqlCommand("call selecionaPedido();", cn.conectarBD());
                MySqlDataReader dr = cmd.ExecuteReader();
                return ListaDePedidos(dr);
            }
        }

        private List<Pedido> ListaDePedidos(MySqlDataReader dr)
        {
            var pedidos = new List<Pedido>();
            while (dr.Read())
            {
                var TempPedido = new Pedido()
                {
                    cod_pedido = Convert.ToInt32(dr[0]),
                    nome_produto = dr[1].ToString(),
                    data = Convert.ToDateTime(dr[2].ToString()),
                    desc_produto = dr[3].ToString(),
                    situacao = dr[4].ToString(),
                    quantidade_pedido = Convert.ToInt32(dr[5]),
                    valor = Math.Round(Convert.ToDouble(dr[6]), 2),
                    tipo_pagamento = dr[7].ToString(),
                    nome_cli = dr[8].ToString(),
                };
                pedidos.Add(TempPedido);
            }
            return pedidos;
        }

        public List<Pedido> ListarIdPedido(int Id)
        {
            MySqlCommand cmd = new MySqlCommand("call selecionaPedidoPorId(@cod)", cn.conectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.Int32).Value = Id;
            MySqlDataReader dr = cmd.ExecuteReader();

            return ListaDePedidos(dr);
        }

    }
}
