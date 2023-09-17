using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ConexaoDao
{
    public class Conexao
    {
        private MySqlConnection conn;

        public MySqlConnection Conectar()
        {
            // Construa a string de conexão
            string stringConexao = $"Server=localhost;User=root;Password=;Database=db_produtos;";
            try
            {
                conn = new MySqlConnection(stringConexao);
                conn.Open();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return conn;

        }

        public void fecharConexao()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
