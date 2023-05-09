using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var categorias = new List<Categoria>();
            var subcategorias = new List<Subcategoria>();

            string opcao = "menu";
            while (opcao == "menu")
            {
                Console.Clear();
                Console.WriteLine("Bem-vindo à lojinha!\n\nO que deseja fazer?");
                Console.Write("\n[1] para Cadastrar Categoria\n[2] para Editar Categoria\n" +
                    "[3] para Cadastar Subcategoria\n[4] para Editar Subcategoria\n" +
                    "[5] para Pesquisar Categorias\n[6] para Pesquisar Subcategorias\n" +
                    "[7] para Remover Categoria\n[8] para Remover Subcategoria\n[9] para Sair\n ");
                opcao = Console.ReadLine();

                //Sai do programa
                if (opcao == "9")
                {
                    return;
                }

                //Criar categoria.
                else if (opcao == "1")
                {
                    try
                    {
                        Categoria.Cadastrar(categorias, subcategorias);
                    }
                    catch (NomeException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    opcao = "menu";
                }

                //Editar categoria, mas não foi cadastrada nenhuma para edição.
                else if(opcao == "2" && categorias.Count == 0)
                {
                    Console.WriteLine("\nNão existem categorias cadastradas para edição. " +
                        "Tecle Enter para retornar ao Menu Principal.");
                    Console.ReadLine();
                    opcao = "menu";
                }

                //Editar categoria.
                else if(opcao == "2" && categorias.Count > 0)
                {
                    try
                    {
                        Categoria.Editar(categorias, subcategorias);
                    }
                    catch(NomeException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    opcao = "menu";
                    }

                //Cadastrar subcategoria, mas não foi cadastrada uma categoria.
                else if(opcao == "3" && categorias.Count == 0)
                {
                    Console.WriteLine("\nCadastre primeiro uma categoria, para então cadastrar uma subcategoria. " +
                        "Tecle Enter para retornar ao Menu Principal.");
                    Console.ReadLine();
                    opcao = "menu";
                }

                //Cadastrar subcategoria.
                else if(opcao == "3" && categorias.Count > 0)
                {
                    try
                    {
                        Subcategoria.Cadastrar(subcategorias, categorias);
                    }
                    catch (NomeException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    catch (StatusException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    opcao = "menu";
                }

                //Editar subcategoria, mas não foi cadastrada uma subcategoria.
                else if(opcao == "4" && subcategorias.Count == 0)
                {
                    Console.WriteLine("\nNão existem subcategorias cadastradas para edição. " +
                        "Tecle Enter para retornar ao Menu Principal.");
                    Console.ReadLine();
                    opcao = "menu";
                }

                //Editar subcategoria.
                else if(opcao == "4" && subcategorias.Count > 0)
                {
                    try
                    {
                        Subcategoria.Editar(subcategorias, categorias);
                    }
                    catch (NomeException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    catch (StatusException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    opcao = "menu";
                }

                //Pesquisar categorias mas não existem categorias.
                else if(opcao == "5" && categorias.Count ==0)
                {
                    Console.WriteLine("\nNão existem categorias cadastradas. " +
                        "Tecle Enter para retornar ao Menu Principal.");
                    Console.ReadLine();
                    opcao = "menu";
                }

                //Pesquisar categorias.
                else if(opcao == "5" && categorias.Count > 0)
                {
                    try
                    {
                        Categoria.Pesquisar(categorias, subcategorias);
                    }
                    catch (NomeException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    opcao = "menu";
                }

                //Pesquisar subcategorias mas não existem subcategorias.
                else if(opcao == "6" && categorias.Count == 0)
                {
                    Console.WriteLine("\nNão existem subcategorias cadastradas. " +
                        "Tecle Enter para retornar ao Menu Principal.");
                    Console.ReadLine();
                    opcao = "menu";
                }

                //Pesquisar subcategorias.
                else if(opcao == "6" && subcategorias.Count > 0)
                {
                    try
                    {
                        Subcategoria.Pesquisar(subcategorias, categorias);
                    }
                    catch (NomeException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    opcao = "menu";
                }

                //Remover categorias mas não existem categorias.
                else if (opcao == "7" && categorias.Count == 0)
                {
                    Console.WriteLine("\nNão existem categorias. " +
                        "Tecle Enter para retornar ao Menu Principal.");
                    Console.ReadLine();
                    opcao = "menu";
                }

                //Remover categorias.
                else if(opcao == "7" && categorias.Count > 0)
                {
                    try
                    {
                        Categoria.Remover(categorias, subcategorias);
                    }
                    catch (NomeException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    opcao = "menu";
                }

                //Remover subcategorias mas não existem subcategorias.
                else if (opcao == "8" && subcategorias.Count == 0)
                {
                    Console.WriteLine("\nNão existem subcategorias. " +
                        "Tecle Enter para retornar ao Menu Principal.");
                    Console.ReadLine();
                    opcao = "menu";
                }

                //Remover subcategorias.
                else if(opcao == "8" && subcategorias.Count > 0)
                {
                    try
                    {
                        Subcategoria.Remover(subcategorias, categorias);
                    }
                    catch (NomeException e)
                    {
                        Console.WriteLine($"{e.Message}\nTecle Enter para retornar ao Menu Principal.");
                        Console.ReadLine();
                    }
                    opcao = "menu";
                }

                else if(opcao == "@todos")
                {
                    Console.Clear();
                    Console.WriteLine("\nCATEGORIAS:\n");
                    if(categorias.Count == 0)
                    {
                        Console.WriteLine("Não há categorias cadastradas.");
                    }
                    else
                    {
                        int i = 0;
                        foreach(Categoria categoria in categorias)
                        {
                            if (i % 2 == 0)
                                Console.Write("- " + String.Format("{0, -20}", categoria.Nome));
                            else
                                Console.WriteLine($"    - {String.Format("{0, -20}", categoria.Nome)}");
                            i++;
                        }
                    }
                    Console.WriteLine("\n\nSUBCATEGORIAS:\n");
                    if(subcategorias.Count == 0)
                    {
                        Console.WriteLine("Não há subcategorias cadastradas.");
                    }
                    else
                    {
                        int j = 0;
                        foreach (Subcategoria subcategoria in subcategorias)
                        {
                            if (j % 2 == 0)
                                Console.Write("- " + String.Format("{0, -20}", subcategoria.Nome));
                            else
                                Console.WriteLine($"    - {String.Format("{0, -20}", subcategoria.Nome)}");
                            j++;
                        }
                    }
                    Console.WriteLine("\nPressione Enter para retornar ao Menu Principal.");
                    Console.ReadLine();
                    Console.Clear();
                    opcao = "menu";
                }

               //Caso alguma opção inválida seja digitada.
                else
                {
                    Console.WriteLine("\nOpção inválida. Pressione Enter para retornar ao Menu Princial.");
                    opcao = "menu";
                    Console.ReadLine();
                }
            }
        }
    }
}
