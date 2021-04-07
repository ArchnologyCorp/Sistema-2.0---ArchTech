using Arch_Tech.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Arch_Tech.Repositório
{
    public class actionsLogin
    {
        conexao cn = new conexao();

        public void testarUsuario(Login login)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbLogin where nome_usuario = @usuario and senha = @senha;", cn.conectarBD());
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = login.nome_usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = login.senha;

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    login.nome_usuario = dr["nome_usuario"].ToString();
                    login.senha = dr["senha"].ToString();
                    login.tipo = Convert.ToInt32(dr["tipo"]);
                }
            }

            else
            {
                login.nome_usuario = null;
                login.senha = null;
                login.tipo = Convert.ToInt32(null);
            }

            cn.desconectarBD();
        }
    }
}