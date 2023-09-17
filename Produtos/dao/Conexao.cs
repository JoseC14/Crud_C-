using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ConexaoDao
{
    public class Conexao
    {
        private MySqlConnection conn;
        //Função para conectar no banco de dados e retornar a conexao
        public MySqlConnection Conectar()
        {
            
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
        // Função para fechar a conexão quando necessário
        public void fecharConexao()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
