using Arch_Tech.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arch_Tech.Repositório
{
    public class actionsProduto
    {
        conexao cn = new conexao();
        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=dblojainformatica; password=0431723748");

        public void cadastraProduto(Produto prod)
        {
            MySqlCommand cmd = new MySqlCommand("call cadastraProduto(@nome,@desc, @qtd, @valor, @cod_fornecedor)", cn.conectarBD());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = prod.nome_produto;
            cmd.Parameters.Add("@desc", MySqlDbType.Text).Value = prod.desc_produto;
            cmd.Parameters.Add("@qtd", MySqlDbType.Int32).Value = prod.quantidade_estoque;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = prod.valor_unitario;
            cmd.Parameters.Add("@cod_fornecedor", MySqlDbType.Int32).Value = prod.cod_fornecedor;

            cmd.ExecuteNonQuery();

            cn.desconectarBD();

        }

        public void consultaBuscaProduto(Produto prod)
        {
            MySqlCommand cmd = new MySqlCommand("call buscaProdutoPorId(@cod)", cn.conectarBD());
            cmd.Parameters.AddWithValue("@cod", MySqlDbType.Int32).Value = prod.cod_produto;
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                prod.nome_produto = dr[1].ToString();
                prod.desc_produto = dr[2].ToString();
                prod.quantidade_estoque = Convert.ToInt32(dr[3]);
                prod.valor_unitario = Convert.ToDouble(dr[4]);
                prod.cod_fornecedor = Convert.ToInt32(dr[5]);
            }
        }

        public void atualizaProduto(Produto prod)
        {
            MySqlCommand cmd = new MySqlCommand("call alteraProduto(@cod,@nome,@desc,@qtd,@valor,@cod_fornecedor)", cn.conectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.Int32).Value = prod.cod_produto;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = prod.nome_produto;
            cmd.Parameters.Add("@desc", MySqlDbType.Text).Value = prod.desc_produto;
            cmd.Parameters.Add("@qtd", MySqlDbType.Text).Value = prod.quantidade_estoque;
            cmd.Parameters.Add("@valor", MySqlDbType.Float).Value = prod.valor_unitario;
            cmd.Parameters.Add("@cod_fornecedor", MySqlDbType.Float).Value = prod.cod_fornecedor;

            cmd.ExecuteNonQuery();
            cn.desconectarBD();
        }

        public void deletaProduto(Produto prod)
        {
            MySqlCommand cmd = new MySqlCommand("call excluirProduto(@cod)", cn.conectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.Int32).Value = prod.cod_produto;

            cmd.ExecuteNonQuery();
            cn.desconectarBD();
        }

        public List<Produto> Listar()
        {
            MySqlCommand cmd = new MySqlCommand("call selecionaProduto();", cn.conectarBD());
            MySqlDataReader dr = cmd.ExecuteReader();
            return ListaDeProdutos(dr);
        }

        private List<Produto> ListaDeProdutos(MySqlDataReader dr)
        {
            var produtos = new List<Produto>();
            while (dr.Read())
            {
                var TempProduto = new Produto()
                {
                    cod_produto = Convert.ToInt32(dr[0]),
                    nome_produto = dr[1].ToString(),
                    valor_unitario = Math.Round(Convert.ToDouble(dr[2]), 2),
                    marca = dr[3].ToString(),
                    quantidade_estoque_lista = dr[4].ToString(),
                };
                produtos.Add(TempProduto);
            }
            return produtos;
        }

        public List<Produto> ListarNome(string nome)
        {
            MySqlCommand cmd = new MySqlCommand("selecionaProdutoPorNome(@nome)", cn.conectarBD());
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
            MySqlDataReader dr = cmd.ExecuteReader();

            return ListaDeProdutos(dr);
        }

        public Produto BuscaId(int Id)
        {
            using (cn.conectarBD())
            {
                MySqlCommand cmd = new MySqlCommand("call selecionaIdDoProduto(@cod)", cn.conectarBD());
                cmd.Parameters.Add("@cod", MySqlDbType.Int32).Value = Id;
                MySqlDataReader dr = cmd.ExecuteReader();

                return ListaDeProdutos(dr).FirstOrDefault();
            }
        }

    }
}