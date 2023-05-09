using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Categorias;
using NomeException;
using SPRINT_4;

namespace DESAFIO4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var categoria = new Categoria();
            var subCategoria = new SubCategoria();

            string opcaoEscolhida = "5";

            while (opcaoEscolhida == "5")

            {
                Console.Clear();
                Console.WriteLine("BEM VINDO!");
                Console.WriteLine();
                Console.WriteLine("Escolha a opção desejada: ");
                Console.WriteLine();
                Console.WriteLine("1 - Cadastrar");
                Console.WriteLine("2 - Editar");
                Console.WriteLine("3 - Consultar");
                Console.WriteLine("0 - Sair");

                Console.WriteLine();

                Console.Write("Digite a opção escolhida: ");
                opcaoEscolhida = Console.ReadKey().KeyChar.ToString(); //transformar a opção em string



                if (opcaoEscolhida == "1")

                {
                    Console.WriteLine("\nO que deseja cadastrar?");
                    Console.Write("\n1 - Categoria");
                    Console.Write("\n2 - SubCategoria");

                    Console.WriteLine();

                    string opcaoCad = "3";
                    Console.Write("\nDigite a opção escolhida: ");
                    opcaoCad = Console.ReadKey().KeyChar.ToString();

                    //PARTE DO MENU CADASTRAR CATEGORIA

                    if (opcaoCad == "1" && categoria.autenticar == true) //Vai autenticar com cadastro feito
                    {
                        try
                        {
                            categoria.CadastrarCategoria(categoria);
                            categoria.autenticar = true;
                        }

                        catch (NomeExceptions)
                        {
                            categoria.autenticar = false;
                            Console.WriteLine("\nO cadastro não foi realizado.");
                            Console.ReadLine();
                        }

                        // repete o MENU após o cadastro
                        opcaoEscolhida = "5";

                    }

                    else if (opcaoCad == "1" && categoria.autenticar == false) //Se não tem cadastro, pede para fazer um novo
                    {
                        try
                        {
                            categoria.CadastrarCategoria(categoria);
                            categoria.autenticar = true;
                        }
                        catch (NomeExceptions)
                        {
                            Console.WriteLine();
                            categoria.autenticar = false;
                            Console.WriteLine("O cadastro não foi realizado.");
                            Console.ReadLine();
                        }

                        // repete o MENU após o cadastro
                        opcaoEscolhida = "5";
                    }



                    //PARTE DE CADASTRAR SUBCATEGORIA

                    else if (opcaoCad == "2" && categoria.autenticar == true) //Vai autenticar se tem uma Categoria cadastrada
                    {
                        try
                        {
                            subCategoria.CadastrarSub(subCategoria, categoria.catlist);
                            subCategoria.autenticar = true;
                        }

                        catch (NomeExceptions e)
                        {
                            subCategoria.autenticar = false;
                            Console.WriteLine(e.Message);
                            Console.WriteLine("O cadastro não foi realizado.");
                            Console.ReadLine();

                        }

                        opcaoEscolhida = "5";
                    }

                    else if (opcaoCad == "2" && categoria.autenticar == false)//Mensagem que deve aparecer quando não há Categoria cadastrada
                    {
                        Console.WriteLine("\nNão há Categoria cadastrada para cadastrar a SubCategoria.");
                        Console.ReadLine();

                        opcaoEscolhida = "5";
                    }

                    else
                    {
                        opcaoCad = "3";
                        Console.WriteLine();
                        Console.WriteLine("\nOpção digitada inválida.");
                        Console.ReadLine();
                        opcaoEscolhida = "5";
                    }


                }

                //PARTE DE EDITAR

                else if (opcaoEscolhida == "2")
                {
                    Console.WriteLine("\nO que deseja editar?");
                    Console.Write("\n1 - Categoria");
                    Console.Write("\n2 - SubCategoria");

                    Console.WriteLine();

                    string opcaoEdit = "3";
                    Console.Write("\nDigite a opção escolhida: ");
                    opcaoEdit = Console.ReadKey().KeyChar.ToString();

                    //PARTE DE EDITAR CATEGORIA

                    if (opcaoEdit == "1" && categoria.autenticar == true)//Vai autenticar se tem uma Categoria cadastrada
                    {
                        try
                        {
                            categoria.EditarCategoria(categoria.catlist);
                            categoria.autenticar = true;
                        }

                        catch (NomeExceptions)
                        {
                            Console.WriteLine();
                            categoria.autenticar = false;

                            Console.WriteLine("O cadastro não foi realizado.");
                            Console.ReadLine();
                        }

                        catch (Exception)

                        {
                            Console.WriteLine("Não há Categorias cadastradas no sistema."); //vai bloquear quando não há nada cadastrado.
                            Console.ReadLine();
                        }

                        opcaoEscolhida = "5";
                    }



                    else if (opcaoEdit == "1" && categoria.autenticar == false)//Mensagem de que não tem Categoria Cadastrada
                    {
                        Console.WriteLine("\nNão há categoria cadastrada para editá-la.");
                        Console.ReadLine();

                        opcaoEscolhida = "5";
                    }


                    //PARTE DE EDITAR SUBCATEGORIA
                    else if (opcaoEdit == "2" && subCategoria.autenticar == true) //Vai autenticar se tem SubCategoria cadastrada
                    {
                        try
                        {
                            subCategoria.EditarSub(subCategoria.sublist);
                            subCategoria.autenticar = true;
                        }

                        catch (NomeExceptions)
                        {
                            Console.WriteLine();
                            subCategoria.autenticar = false;

                            Console.WriteLine("O cadastro não foi realizado.");
                            Console.ReadLine();
                        }

                        catch (Exception)

                        {
                            Console.WriteLine("Não há SubCategorias cadastradas no sistema."); //vai bloquear quando não há nada cadastrado.
                            Console.ReadLine();
                        }
                        opcaoEscolhida = "5";
                    }



                    else if (opcaoEscolhida == "2" && subCategoria.autenticar == false) //Mensagem de quando não há SubCategoria cadastrada
                    {
                        Console.WriteLine("\nNão há Subcategoria cadastrada para editá-la.");
                        Console.ReadLine();

                        opcaoEscolhida = "5";

                    }


                    else
                    {
                        opcaoEdit = "3";
                        Console.WriteLine();
                        Console.WriteLine("\nOpção digitada inválida.");
                        Console.ReadLine();
                        opcaoEscolhida = "5";
                    }


                }

                //CONSULTAR 

                else if (opcaoEscolhida == "3")

                {
                    Console.WriteLine("\nO que deseja consultar?");
                    Console.Write("\n1 - Categoria");
                    Console.Write("\n2 - SubCategoria");

                    Console.WriteLine();

                    string opcaoConsult;
                    Console.Write("\nDigite a opção escolhida: ");
                    opcaoConsult = Console.ReadKey().KeyChar.ToString();

                    //PARTE DE CONSULTAR CATEGORIA

                    if (opcaoConsult == "1")

                    {
                        try
                        {
                            categoria.ConsultarCategoria(categoria.catlist); //vai tentar consultar
                        }

                        catch (NomeExceptions e)
                        {
                            Console.WriteLine(e.Message); //vai bloquear quando o nome não é encontrado
                            Console.ReadLine();
                        }

                        catch (Exception)

                        {
                            Console.WriteLine("Não há Categorias cadastradas no sistema."); ; //vai bloquear quando não há nada cadastrado.
                            Console.ReadLine();
                        }
                    }

                    //PARTE DE CONSULTAR SUBCATEGORIA

                    else if (opcaoConsult == "2")
                    {
                        try
                        {
                            subCategoria.ConsultarSub(subCategoria.sublist); //vai tentar consultar
                        }

                        catch (NomeExceptions e)
                        {
                            Console.WriteLine(e.Message); //vai bloquear quando o nome não é encontrado
                            Console.ReadLine();
                        }

                        catch (Exception)

                        {
                            Console.WriteLine("Não há SubCategorias cadastradas no sistema."); //vai bloquear quando não há nada cadastrado.
                            Console.ReadLine();
                        }


                    }

                    opcaoEscolhida = "5";
                }



                //OUTRA OPÇÃO

                else //Se não for a opção, aparece a mensagem
                {
                    opcaoEscolhida = "5";
                    Console.WriteLine();
                    Console.WriteLine("\nOpção digitada inválida.");
                    Console.ReadLine();

                }


            }
        }
    }
}
