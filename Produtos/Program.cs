using Produto.Dto;
using Produto.Dao;
using System.Collections.Generic;
using System;
class Program
{
    static void Main()
    {
        int opcao,id;
        Console.WriteLine("************************\n");
        Console.WriteLine("* CADASTRO DE PRODUTOS *\n");
        Console.WriteLine("************************\n\n");

        while (true)
        {
            menuPrincipal:
            Console.WriteLine("Escolha uma opção\n");
            Console.WriteLine("1 - Cadastrar Produtos\n");
            Console.WriteLine("2 - Gerenciar Produtos\n");

            opcao = Convert.ToInt32(Console.ReadLine());
            ProdutoDao produtoDao = new ProdutoDao();
            if (opcao == 1)
            {
                Console.WriteLine("*********************\n");
                Console.WriteLine("* Cadastrar Produto *\n");
                Console.WriteLine("*********************\n");

                ProdutoDto produto = new ProdutoDto();

                Console.WriteLine("Digite o nome");
                produto.Nome = Console.ReadLine();

                Console.WriteLine("Digite o preço");
                produto.Preco = float.Parse(Console.ReadLine());

                Console.WriteLine("Digite a validade");
                produto.Validade = Console.ReadLine();

                Console.WriteLine("Digite a categoria");
                produto.Categoria = Console.ReadLine();



                produtoDao.inserirProduto(produto);
                





            }
            else if(opcao == 2)
            {
                Console.WriteLine("**********************\n");
                Console.WriteLine("* Gerenciar Produtos *\n");
                Console.WriteLine("**********************\n");

                listagemProdutos:
                produtoDao.listarProdutos();
                Console.WriteLine("Digite o ID para fazer mais operações (Deletar ou Alterar) ou 0 para voltar ao menu principal");
                id = Convert.ToInt32(Console.ReadLine());
                
                while (true)
                {
                    Console.WriteLine("Digite a operação (1 - Deletar; 2 - Atualizar) ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                    if(opcao == 0)
                    {
                        goto menuPrincipal;
                    }
                    else if(opcao == 1)
                    {
                        produtoDao.deletarProduto(id);
                        goto listagemProdutos;
                    }else if(opcao == 2)
                    {
                        ProdutoDto produtoDto = new ProdutoDto();
                        List<object> lista =  produtoDao.getProdutoById(id);
                        Console.WriteLine("Nome");
                        produtoDto.Nome = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(produtoDto.Nome))
                        {
                            produtoDto.Nome = (string?)lista[0];
                        }

                        Console.WriteLine("Preço");
                        produtoDto.Preco = float.Parse(Console.ReadLine());

                        if (string.IsNullOrWhiteSpace(produtoDto.Preco.ToString()))
                        {
                            produtoDto.Preco = (float)lista[1];
                        }

                        Console.WriteLine("Validade");
                        produtoDto.Validade = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(produtoDto.Validade))
                        {
                            produtoDto.Validade = (string?)lista[2];
                        }

                        Console.WriteLine("Categoira");
                        produtoDto.Categoria = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(produtoDto.Categoria))
                        {
                            produtoDto.Categoria = (string?)lista[2];
                        }

                        produtoDao.alterarProduto(produtoDto,id);


                    }
                    else
                    {
                        Console.WriteLine("Opção Inválida");
                    }
                }
                


            }
            else
            {
                Console.WriteLine("Opção Inválida\n");
            }


        }
    }
}

