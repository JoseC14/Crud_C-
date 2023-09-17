using Produto.Dto;
using Produto.Dao;
using System.Collections.Generic;
using System;

//CRUD DE PRODUTOS - CONSOLE APPLICATION
//Autor : José Carlos Pereira Vieira
class Program
{
    static void Main()
    {
        //Declaração de variáveis
        int opcao,id;
        float preco;
        Console.WriteLine("************************\n");
        Console.WriteLine("* CADASTRO DE PRODUTOS *\n");
        Console.WriteLine("************************\n\n");

        while (true)
        {
            //MENU PRINCIPAL
            menuPrincipal:
            Console.WriteLine("Escolha uma opção\n");
            Console.WriteLine("1 - Cadastrar Produtos\n");
            Console.WriteLine("2 - Gerenciar Produtos\n");
            Console.WriteLine("3 - Sair do Programa\n");

            //Tenta converter o número para inteiro, se não conseguir, repete o menu principal e pede
            //ao usuário para digitar novamente
            if (!int.TryParse(Console.ReadLine(), out opcao)){
                Console.WriteLine("Entrada Inválida. Digite um número inteiro");
                continue;
            };
             
            
            ProdutoDao produtoDao = new ProdutoDao();
            //Se a opção digita for um ele irá para o cadastro de produtos
            if (opcao == 1)
            {
                //CADASTRO DE PRODUTOS
                cadProdutos:
                Console.WriteLine("*********************\n");
                Console.WriteLine("* Cadastrar Produto *\n");
                Console.WriteLine("*********************\n");

                ProdutoDto produto = new ProdutoDto();

                Console.WriteLine("Digite o nome");
                produto.Nome = Console.ReadLine();

                Console.WriteLine("Digite o preço");
                //Tenta parsear o que o usuário digitou para float, senão conseguir irá pedir para digitar 
                //novamente, se conseguir irá atribuir o valor de preco a produto.Preco
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


                //Insere o produto
                produtoDao.inserirProduto(produto);
                





            }
            //Se a opção digitada for 2 irá para o menu de gerenciamento de produtos
            else if(opcao == 2)
            {
                Gerenciamento:
                Console.WriteLine("**********************\n");
                Console.WriteLine("* Gerenciar Produtos *\n");
                Console.WriteLine("**********************\n");

                listagemProdutos:
                produtoDao.listarProdutos();
                Console.WriteLine("Digite o ID para fazer mais operações (Deletar ou Alterar) ou digite qualquer letra para voltar ao menu principal");
                //Tenta converter a opção digitada para int se nao conseguir, vai para o menu principal
                if (!int.TryParse(Console.ReadLine(),out id))
                {
                    goto menuPrincipal;
                }

                while (true)
                {
                    //MENU DE OPERAÇÕES

                    Console.WriteLine("Digite a operação (1 - Deletar; 2 - Atualizar; 3 - Voltar para o gerenciamento) ");
                    opcao = Convert.ToInt32(Console.ReadLine());
                    //Se a opção informada for 1 deleta o produto, se for 2 atualiza, se for 3 volta
                    //para a tela de gerenciamento
                     if(opcao == 1)
                    {
                        produtoDao.deletarProduto(id);
                        goto listagemProdutos;
                    }else if(opcao == 2)
                    {
                        ProdutoDto produtoDto = new ProdutoDto();
                        //Lista para pegar os valores do produto
                        List<object> lista =  produtoDao.getProdutoById(id);
                        Console.WriteLine("Nome(Deixe Vazio para não mudar)");
                        produtoDto.Nome = Console.ReadLine();
                        //Se a string for nula ou vazia ele ira inserir os valores que existentes no banco
                        if (string.IsNullOrWhiteSpace(produtoDto.Nome))
                        {
                            produtoDto.Nome = (string?)lista[1];
                        }
                        updPreco:
                        Console.WriteLine("Preço(Deixe Vazio para não mudar)");
                        string precoStr = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(precoStr.ToString()))
                        {
                            produtoDto.Preco = (float)lista[2];
                        }
                        else
                        {
                            //Cria a variável temporária tempPreco e tenta parsear precoStr para float
                            //Se conseguir, armazenar o preco no produtoDto
                            float tempPreco;
                            if (Single.TryParse(precoStr, out tempPreco))
                            {
                                produtoDto.Preco = tempPreco;
                            }
                            else
                            {
                                Console.WriteLine("Preço Inválido!");
                                goto updPreco;
                            }
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
                        //Altera o produto
                        produtoDao.alterarProduto(produtoDto,id);


                    }else if(opcao == 3)
                    {
                        //Voita para a tela de gerenciamento
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

