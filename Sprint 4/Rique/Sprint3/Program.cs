using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Categoria cat = new Categoria();
            Subcategoria sub = new Subcategoria();

            Console.WriteLine("Bem-vindo à lojinha!");

            string opcao = "menu";
            while (opcao == "menu")
            {
                Console.WriteLine("\nO que deseja fazer?");
                Console.Write("\n[1] para Cadastrar Categoria\n[2] para Editar Categoria\n[3] para Cadastar Subcategoria\n[4] para Editar Subcategoria\n[5] para Visualizar\n[6] para Sair\n ");
                opcao = Console.ReadLine();

                //Sai do programa
                if (opcao == "6")
                {
                    return;
                }

                //Criar categoria, se um cadastro não tiver acontecido anteriormente.
                else if (opcao == "1" && cat.cadastro == false)
                {
                    try
                    {
                        cat.CadastrarCategoria();
                        cat.cadastro = true;
                    }
                    catch (NomeException)
                    {
                        cat.cadastro = false;
                    }
                    opcao = "menu";
                }

                //Criar categoria, mas já existe um cadastro feito.
                else if (opcao == "1" && cat.cadastro == true)
                {
                    string mostrasts;
                    if (cat.Status == true)
                        mostrasts = "Ativa";
                    else
                        mostrasts = "Inativa";

                    Console.Clear();
                    Console.WriteLine("Já existe uma categoria cadastrada: " + cat.Nome + ". No momento, ela está " + mostrasts + ".");

                    bool loop = true;
                    while (loop == true)
                    {
                        Console.Write("\nDeseja substituí-la [1], editá-la [2], visualizá-la [3] ou voltar ao menu principal [4]?  ");
                        opcao = Console.ReadLine();

                        if (opcao == "4") //Sai do loop while e volta ao loop anterior ao setar a variável opcao como menu.
                        {
                            loop = false;
                            opcao = "menu";
                        }
                        else if (opcao == "1") //Substitui o cadastro por um novo.
                        {
                            if (sub.cadastro == false) // Cadastra uma nova Categoria se não existir uma Subcategoria cadastrada anteriormente.
                            {
                                try
                                {
                                    cat.CadastrarCategoria();
                                }
                                catch (NomeException) { }
                                loop = false;
                                opcao = "menu";
                            }
                            else // Informa que existe uma Subcategoria atrelada à Categoria, e se criar uma nova Categoria, a Subcategoria existente será apaga.
                            {
                                while (loop == true)
                                {
                                    Console.Write("Existe uma subcategoria cadastrada: " + sub.Nome + ". Se continuar, ela será excluída.\nDeseja prosseguir? [S] Sim ou [N] Não     ");
                                    opcao = Console.ReadLine();
                                    if (opcao == "N")
                                    {
                                        opcao = "menu";
                                        loop = false;
                                    }
                                    else if (opcao == "S")
                                    {
                                        try
                                        {
                                            cat.CadastrarCategoria();
                                            sub.Apagar();
                                        }
                                        catch (NomeException) { }
                                        loop = false;
                                        opcao = "menu";
                                    }
                                    else
                                        Console.WriteLine("Opção inválida.");
                                }
                            }
                        }

                        else if (opcao == "2") //Edita a categoria que já foi feita.
                        {
                            try
                            {
                                cat.EditarCategoria(sub);
                            }
                            catch (NomeException) { }
                            loop = false;
                            opcao = "menu";
                        }

                        else if (opcao == "3") //Vizualiza o que foi salvo.
                        {
                            opcao = "menu";
                            loop = false;
                            MostrarCategoriaESubcategoria(cat, sub);
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida. Tente novamente.\n");
                        }
                    }

                }

                //Editar categoria, mas ainda não foi feito nenhum cadastro para ser editado.
                else if (opcao == "2" && cat.cadastro == false)
                {
                    Console.WriteLine("\nNão existem categorias cadastradas. Favor cadastre uma categoria para poder editar.");
                    opcao = "menu";
                }

                //Editar categoria, e já foi feito um cadastro anteriormente.
                else if (opcao == "2" && cat.cadastro == true)
                {
                    try
                    {
                        cat.EditarCategoria(sub);
                    }
                    catch (NomeException) { }
                    opcao = "menu";
                }

                //Cadastrar Subcategoria, mas não foi cadastrada Categoria anteriormente.
                else if (opcao == "3" && cat.cadastro == false)
                {
                    Console.WriteLine("\nNão existe Categoria cadastrada. Para criar uma Subcategoria, cadastre uma Categoria primeiro.");
                    opcao = "menu";
                }

                //Cadastrar Subcategoria e existe Categoria, mas não existe Subcategoria.
                else if (opcao == "3" && cat.cadastro == true && sub.cadastro == false)
                {
                    try
                    {
                        sub.CadastrarSubcategoria(cat);
                        sub.cadastro = true;
                    }
                    catch (NomeException)
                    {
                        sub.cadastro = false;
                    }
                    opcao = "menu";
                }

                //Cadastrar Subcategoria e já existe uma Subcategoria cadastrada.
                else if (opcao == "3" && sub.cadastro == true)
                {
                    string mostrastsSubcategoria;
                    if (cat.Status == true)
                        mostrastsSubcategoria = "Ativa";
                    else
                        mostrastsSubcategoria = "Inativa";

                    Console.Clear();
                    Console.WriteLine("Já existe uma subcategoria cadastrada: " + sub.Nome + ". No momento, ela está " + mostrastsSubcategoria + " e pertence à categoria " + cat.Nome + ".");

                    bool loop = true;
                    while (loop == true)
                    {
                        Console.Write("\nDeseja substituí-la [1], editá-la [2], visualizá-la [3] ou voltar ao menu principal [4]?  ");
                        opcao = Console.ReadLine();

                        if (opcao == "4") //Sai do loop while e volta ao loop anterior ao setar a variável opcao como menu.
                        {
                            loop = false;
                            opcao = "menu";
                        }
                        else if (opcao == "1") //Substitui o cadastro da subcategoria por um novo.
                        {
                            try
                            {
                                sub.CadastrarSubcategoria(cat);
                            }
                            catch (NomeException) { }
                            loop = false;
                            opcao = "menu";
                        }

                        else if (opcao == "2") //Edita a subcategoria que já foi feita.
                        {
                            try
                            {
                                sub.EditarSubcategoria(cat);
                            }
                            catch (NomeException) { }
                            loop = false;
                            opcao = "menu";
                        }

                        else if (opcao == "3") //Vizualiza o que foi salvo.
                        {
                            opcao = "menu";
                            loop = false;
                            MostrarCategoriaESubcategoria(cat, sub);
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida. Tente novamente.\n");
                        }
                    }
                }

                //Editar Subcategoria, e não existe Categoria cadastrada
                else if (opcao == "4" && cat.cadastro == false)
                {
                    Console.WriteLine("\nNão existe Categoria cadastrada. Para cadastrar e editar uma Subcategoria, é necessário cadastrar uma Categoria.");
                    opcao = "menu";
                }

                //Editar Subcategoria, e existe Categoria cadastrada, mas não existe Subcategoria cadastrada
                else if (opcao == "4" && cat.cadastro == true && sub.cadastro == false)
                {
                    Console.WriteLine("\nNão existe Subcategoria cadastrada. Cadastre uma para poder editar.");
                    opcao = "menu";
                }

                //Editar Subcategoria e existe Categoria e Subcategoria cadastradas
                else if (opcao == "4" && cat.cadastro == true && sub.cadastro == true)
                {
                    try
                    {
                        sub.EditarSubcategoria(cat);
                    }
                    catch (NomeException) { }
                    opcao = "menu";
                }

                //Visualizar categoria, mas não foi feito um cadastro.
                else if (opcao == "5" && cat.cadastro == false)
                {
                    Console.WriteLine("\nNão existem categorias cadastradas. Favor cadastre uma categoria para poder visualizar.");
                    opcao = "menu";
                }

                // Visualizar categoria, e categoria foi cadastrada.
                else if (opcao == "5" && cat.cadastro == true)
                {
                    MostrarCategoriaESubcategoria(cat, sub);
                    opcao = "menu";
                }

                //Situação em que foi digitado uma opção inválida.
                else
                {
                    Console.WriteLine("\nOpção inválida. Tente novamente.");
                    opcao = "menu";
                }
            }
        }

        //VISUALIZAR
        static void MostrarCategoriaESubcategoria(Categoria categoria, Subcategoria subcategoria)
        {
            string mostrastsCategoria;
            if (categoria.Status == true)
                mostrastsCategoria = "Ativa";
            else
                mostrastsCategoria = "Inativa";

            string mostrastsSubcategoria;
            if (subcategoria.Status == true)
                mostrastsSubcategoria = "Ativa";
            else
                mostrastsSubcategoria = "Inativa";

            Console.Clear();
            Console.WriteLine("VISUALIZAÇÃO DE CATEGORIA\n\n");
            Console.WriteLine("NOME DA CATEGORIA:   " + categoria.Nome);
            Console.WriteLine("STATUS DA CATEGORIA: " + mostrastsCategoria);
            Console.WriteLine("DATA DA CRIAÇÃO:     " + categoria.DataEHoraCriacao);
            if (categoria.DataEHoraModificacao == categoria.valorPadrao)
                Console.WriteLine("DATA DE MODIFICAÇÃO: Não houve alteração.");
            else
                Console.WriteLine("DATA DE MODIFICAÇÃO: " + categoria.DataEHoraModificacao);

            Console.WriteLine("\n\nVISUALIZAÇÃO DE SUBCATEGORIA\n\n");

            if (subcategoria.cadastro == true)
            {
                Console.WriteLine("NOME DA SUBCATEGORIA:    " + subcategoria.Nome);
                Console.WriteLine("STATUS DA SUBCATEGORIA:  " + mostrastsSubcategoria);
                Console.WriteLine("DATA DA CRIAÇÃO:         " + subcategoria.DataEHoraCriacao);
                if (subcategoria.DataEHoraModificacao == categoria.valorPadrao)
                    Console.WriteLine("DATA DE MODIFICAÇÃO:     Não houve alteração.");
                else
                    Console.WriteLine("DATA DE MODIFICAÇÃO:     " + subcategoria.DataEHoraModificacao);
            }
            else
                Console.WriteLine("Não foi criada uma subcategoria.\n");

            Console.WriteLine("\nTecle Enter para continuar");
            Console.ReadLine();

        }
    }
}
