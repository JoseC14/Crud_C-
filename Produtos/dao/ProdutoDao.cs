using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Collections.Generic;
using ConexaoDao;
using Produto.Dto;
namespace Produto.Dao
{
    public class ProdutoDao
    {
        
        public void inserirProduto(ProdutoDto produto)
        {
            Conexao conexao = new Conexao();

            string Query = "INSERT INTO tb_produtos(nome,preco,validade,categoria) VALUES (@nome, @preco, @validade, @categoria)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());

                cmd.Parameters.AddWithValue("@nome", produto.Nome);
                cmd.Parameters.AddWithValue("@preco", produto.Preco);
                cmd.Parameters.AddWithValue("@validade", produto.Validade);
                cmd.Parameters.AddWithValue("@categoria", produto.Categoria);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Preoduto Inserido!");
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            conexao.fecharConexao();

            
        }

        public void listarProdutos()
        {
            Conexao conexao = new Conexao();
            string Query = "SELECT * FROM tb_produtos";
            Console.WriteLine("\tID\tNome\tPreço\tValidade\tCategoria");
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());

                cmd.ExecuteNonQuery();
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nome      = dr.GetString(1);
                    float preco      = dr.GetFloat(2);
                    string validade  = dr.GetString(3);
                    string categoria = dr.GetString(4);
                    Console.WriteLine($"\t{id}\t{nome}\t{preco}\t{validade}\t{categoria}");
                }

                conexao.fecharConexao();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }


            conexao.fecharConexao();
        }

        public List<object> getProdutoById(int id)
        {
            Conexao conexao = new Conexao();
            string Query = "SELECT * FROM tb_produtos WHERE id = "+id;
            try
            {
                List<object> produto = new List<object>();
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());

                cmd.ExecuteNonQuery();
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    produto.Add(dr.GetInt32(0));
                    produto.Add(dr.GetString(1));
                    produto.Add(dr.GetFloat(2));
                    produto.Add(dr.GetString(3));
                    produto.Add(dr.GetString(4));
                }

                return produto;
 
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                List<object> lista = new List<object>();
                return lista;
            }


            conexao.fecharConexao();
        }

        public void deletarProduto(int id)
        {
            Conexao conexao = new Conexao();
            string Query = "DELETE FROM tb_produtos WHERE id = " + id;
            Console.WriteLine("\tID\tNome\tPreço\tValidade\tCategoria");
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());

                cmd.ExecuteNonQuery();
                Console.WriteLine("Produto deletado com sucesso");

                conexao.fecharConexao();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                
            }


            conexao.fecharConexao();

        }
        public void alterarProduto(ProdutoDto produto, int id)
        {
            Conexao conexao = new Conexao();
            string Query = $"UPDATE tb_produtos SET nome='{produto.Nome}',preco = " +
                $"{produto.Preco},validade = '{produto.Validade}', categoria = '{produto.Categoria}' WHERE id = {id} ";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());

                cmd.ExecuteNonQuery();
                Console.WriteLine("Produto alterado com sucesso");

                conexao.fecharConexao();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);

            }


            conexao.fecharConexao();

        }
    }
}
