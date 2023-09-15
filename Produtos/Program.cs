using Produto.Dto;
using Produto.Dao;
using System.Collections.Generic;
using System;
class Program
{
    static void Main()
    {
        int opcao,id;
        char voltar;
        float preco;
        Console.WriteLine("************************\n");
        Console.WriteLine("* CADASTRO DE PRODUTOS *\n");
        Console.WriteLine("************************\n\n");

        while (true)
        {
            menuPrincipal:
            Console.WriteLine("Escolha uma opção\n");
            Console.WriteLine("1 - Cadastrar Produtos\n");
            Console.WriteLine("2 - Gerenciar Produtos\n");
            Console.WriteLine("3 - Sair do Programa\n");

            if (!int.TryParse(Console.ReadLine(), out opcao)){
                Console.WriteLine("Entrada Inválida. Digite um número inteiro");
                continue;
            };
             
            
            ProdutoDao produtoDao = new ProdutoDao();
            if (opcao == 1)
            {
                cadProdutos:
                Console.WriteLine("*********************\n");
                Console.WriteLine("* Cadastrar Produto *\n");
                Console.WriteLine("*********************\n");

                ProdutoDto produto = new ProdutoDto();

                Console.WriteLine("Digite o nome");
                produto.Nome = Console.ReadLine();

                Console.WriteLine("Digite o preço");
                if (!float.TryParse(Console.ReadLine(), out preco))
                {
                    Console.WriteLine("Preço inválido!");
                    goto cadProdutos;
                }
                else
                {
                    produto.Preco = preco;

                }

                Console.WriteLine("Digite a validade");
                produto.Validade = Console.ReadLine();

                Console.WriteLine("Digite a categoria");
                produto.Categoria = Console.ReadLine();



                produtoDao.inserirProduto(produto);
                





            }
            else if(opcao == 2)
            {
                Gerenciamento:
                Console.WriteLine("**********************\n");
                Console.WriteLine("* Gerenciar Produtos *\n");
                Console.WriteLine("**********************\n");

                listagemProdutos:
                produtoDao.listarProdutos();
                Console.WriteLine("Digite o ID para fazer mais operações (Deletar ou Alterar) ou digite qualquer letra para voltar ao menu principal");
                if (!int.TryParse(Console.ReadLine(),out id))
                {
                    goto menuPrincipal;
                }



                if(id == 0)
                {
                    goto menuPrincipal;
                }
                while (true)
                {
                    Console.WriteLine("Digite a operação (1 - Deletar; 2 - Atualizar; 3 - Voltar para o gerenciamento) ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                     if(opcao == 1)
                    {
                        produtoDao.deletarProduto(id);
                        goto listagemProdutos;
                    }else if(opcao == 2)
                    {
                        ProdutoDto produtoDto = new ProdutoDto();
                        List<object> lista =  produtoDao.getProdutoById(id);
                        Console.WriteLine("Nome(Deixe Vazio para não mudar)");
                        produtoDto.Nome = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(produtoDto.Nome))
                        {
                            produtoDto.Nome = (string?)lista[1];
                        }

                        Console.WriteLine("Preço(Deixe Vazio para não mudar)");
                        string precoStr = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(precoStr.ToString()))
                        {
                            produtoDto.Preco = (float)lista[2];
                        }
                        else
                        {
                            produtoDto.Preco = float.Parse(precoStr);
                        }

                        Console.WriteLine("Validade(Deixe Vazio para não mudar)");
                        produtoDto.Validade = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(produtoDto.Validade))
                        {
                            produtoDto.Validade = (string?)lista[3];
                        }

                        Console.WriteLine("Categoria(Deixe Vazio para não mudar)");
                        produtoDto.Categoria = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(produtoDto.Categoria))
                        {
                            produtoDto.Categoria = (string?)lista[4];
                        }

                        produtoDao.alterarProduto(produtoDto,id);


                    }else if(opcao == 3)
                    {
                        goto Gerenciamento;
                    }
                    else
                    {
                        Console.WriteLine("Opção Inválida");
                    }
                }
                


            }else if(opcao == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("Opção Inválida\n");
            }


        }
    }
}

