using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Collections.Generic;
using ConexaoDao;
using Produto.Dto;
using ConsoleTables;

namespace Produto.Dao
{
    public class ProdutoDao
    {
        // Função para adicionar produtos
        public void inserirProduto(ProdutoDto produto)
        {
            Conexao conexao = new Conexao();
            //string sql
            string Query = "INSERT INTO produtos(nome,preco,validade,categoria) VALUES (@nome, @preco, @validade, @categoria)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());
                //Substituindo os parametros
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
            
            //Fechando a conexao após a inserção
            conexao.fecharConexao();

            
        }
        //Função para listar os produtos
        public void listarProdutos()
        {
            Conexao conexao = new Conexao();
            //String sql para listar os produtos
            string Query = "SELECT * FROM produtos";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());

                cmd.ExecuteNonQuery();
                MySqlDataReader dr = cmd.ExecuteReader();
                //Criando uma tabela para popular os dados
                var tabela = new ConsoleTable("ID", "Nome", "Preço", "validade", "Categoria");

                //Loop para adicionar os dados na tabela
                while (dr.Read())
                {
                    int id = dr.GetInt32(0);
                    string nome      = dr.GetString(1);
                    float preco      = dr.GetFloat(2);
                    string validade  = dr.GetString(3);
                    string categoria = dr.GetString(4);
                    tabela.AddRow(id, nome, preco, validade, categoria);
                }
                //Escrevendo a tabela no Console
                tabela.Write();

                //Fechando a conexão
                conexao.fecharConexao();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }


            conexao.fecharConexao();
        }
        //Função para pegar um produto pelo Id(Necessário para função de update)
        public List<object> getProdutoById(int id)
        {
            
            Conexao conexao = new Conexao();
            //string sql para pegar o produto pelo id
            string Query = "SELECT * FROM produtos WHERE id = "+id;
            try
            {
                //Criando lista para armazenar os valores
                List<object> produto = new List<object>();
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());

                cmd.ExecuteNonQuery();
                MySqlDataReader dr = cmd.ExecuteReader();

                //Adicionando os valores para na lista
                while (dr.Read())
                {
                    produto.Add(dr.GetInt32(0));
                    produto.Add(dr.GetString(1));
                    produto.Add(dr.GetFloat(2));
                    produto.Add(dr.GetString(3));
                    produto.Add(dr.GetString(4));
                }
                //Fechando a conexão
                conexao.fecharConexao();
                //Por fim, retornando o produto
                return produto;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                List<object> lista = new List<object>();
                return lista;
            }


            
        }
        //Função para deletar o produto pelo id
        public void deletarProduto(int id)
        {
            Conexao conexao = new Conexao();
            //String sql para deletar os produtos pelo id
            string Query = "DELETE FROM produtos WHERE id = " + id;
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

        }
        //Função para alterar os produtos
        public void alterarProduto(ProdutoDto produto, int id)
        {
            Conexao conexao = new Conexao();
            //String sql de alteraçaõ
            string Query = $"UPDATE produtos SET nome='{produto.Nome}',preco = " +
                $"{produto.Preco},validade = '{produto.Validade}', categoria = '{produto.Categoria}' WHERE id = {id} ";
            try
            {
                MySqlCommand cmd = new MySqlCommand(Query, conexao.Conectar());

                cmd.ExecuteNonQuery();
                Console.WriteLine("Produto alterado com sucesso");
                //Fechando a conexão
                conexao.fecharConexao();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            } 

        }
    }
}
