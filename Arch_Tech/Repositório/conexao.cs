using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Arch_Tech.Repositório
{
    public class conexao
    {
        MySqlConnection cn = new MySqlConnection("server=localhost;user=root;database=dblojainformatica; password=0431723748");
        public static string msg;

        public MySqlConnection conectarBD() {
            try
            {
                cn.Open();
            }

            catch(Exception e)
            {
                msg = "Ocorreu um erro ao se conectar: " + e.Message;
            }

            return cn;
        } 
        public MySqlConnection desconectarBD() {
            try
            {
                cn.Close();
            }

            catch(Exception e)
            {
                msg = "Ocorreu um erro ao se conectar: " + e.Message;
            }

            return cn;
        }


    }
}