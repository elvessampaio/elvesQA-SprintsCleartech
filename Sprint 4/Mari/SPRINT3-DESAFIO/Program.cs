using System;
using Categorias;
using SubCategorias;
using NomeException;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SPRINT3_DESAFIO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //instância local para chamar os objetos das classes criadas

            Categoria cateLocal = new Categoria();
            SubCategoria subLocal = new SubCategoria();
            Categoria editCatLocal = new Categoria();
            SubCategoria editSubLocal = new SubCategoria();


            //Opção escolhida

            string opcaoEscolhida = "5";

            while (opcaoEscolhida == "5")
            {
                //MENU

                Console.Clear();
                Console.WriteLine("BEM VINDO!");
                Console.WriteLine();
                Console.WriteLine("Escolha a opção desejada: ");
                Console.WriteLine();
                Console.WriteLine("1 - Cadastrar Categoria");
                Console.WriteLine("2 - Cadastrar SubCategoria");
                Console.WriteLine("3 - Editar Categoria");
                Console.WriteLine("4 - Editar SubCategoria");
                Console.WriteLine("0 - Sair");

                Console.WriteLine();

                Console.Write("Digite a opção escolhida: ");
                opcaoEscolhida = Console.ReadKey().KeyChar.ToString(); //transformar a opção em string


                if (opcaoEscolhida == "1" && cateLocal.autenticar == true) //Vai autenticar com cadastro feito
                {

                    
                    try
                    {
                        CadastrarCategoria(cateLocal);
                        cateLocal.autenticar = true;
                    }
                    catch (NomeExceptions)
                    {
                        cateLocal.autenticar = false;
                        Console.WriteLine("O cadastro não foi realizado.");
                        Console.ReadLine();
                    }

                    // repete o MENU após o cadastro
                    opcaoEscolhida = "5";

                }

                else if (opcaoEscolhida == "1" && cateLocal.autenticar == false) //Se não tem cadastro, pede para fazer um novo
                {
                    try
                    {
                        CadastrarCategoria(cateLocal);
                        cateLocal.autenticar = true;
                    }
                    catch (NomeExceptions)
                    {
                        Console.WriteLine();
                        cateLocal.autenticar = false;
                        Console.WriteLine("O cadastro não foi realizado.");
                        Console.ReadLine();
                    }

                    // repete o MENU após o cadastro
                    opcaoEscolhida = "5";
                }

                else if (opcaoEscolhida == "2" && cateLocal.autenticar == true) //Vai autenticar se tem uma Categoria cadastrada
                {
                    try
                    {
                        CadastrarSub(subLocal, cateLocal);
                        subLocal.autenticar = true;
                    }

                    catch (NomeExceptions)
                    {
                        subLocal.autenticar = false;
                        Console.WriteLine("O cadastro não foi realizado.");
                        Console.ReadLine();

                    }

                    opcaoEscolhida = "5";
                }

                else if (opcaoEscolhida == "2" && cateLocal.autenticar == false)//Mensagem que deve aparecer quando não há Categoria cadastrada
                {
                    Console.WriteLine("\nNão há Categoria cadastrada para cadastrar a SubCategoria.");
                    Console.ReadLine();

                  opcaoEscolhida = "5";
                }

                else if (opcaoEscolhida == "3" && cateLocal.autenticar == true)//Vai autenticar se tem uma Categoria cadastrada
                {
                    try
                    {
                        EditarCategoria(cateLocal, editCatLocal);
                        editCatLocal.autenticar = true;
                    }

                    catch (NomeExceptions)
                    {
                        Console.WriteLine();
                        editCatLocal.autenticar = false;
                        Console.WriteLine("O cadastro não foi realizado.");
                        Console.ReadLine();
                    }

                    opcaoEscolhida = "5";
                }

                else if (opcaoEscolhida == "3" && cateLocal.autenticar == false)//Mensagem de que não tem Categoria Cadastrada
                {
                    Console.WriteLine("\nNão há categoria cadastrada para editá-la.");
                    Console.ReadLine();

                    opcaoEscolhida = "5";
                }

                else if (opcaoEscolhida == "4" && subLocal.autenticar == true) //Vai autenticar se tem SubCategoria cadastrada
                {
                    try
                    {
                        EditarSub(subLocal, editSubLocal);
                        editSubLocal.autenticar = true;
                    }

                    catch (NomeExceptions)
                    {
                        Console.WriteLine();
                        editSubLocal.autenticar = false;
                        Console.WriteLine("O cadastro não foi realizado.");
                        Console.ReadLine();
                    }
                    opcaoEscolhida = "5";
                }

                else if (opcaoEscolhida == "4" && subLocal.autenticar == false) //Mensagem de quando não há SubCategoria cadastrada
                {
                    Console.WriteLine("\nNão há Subcategoria cadastrada para editá-la.");
                    Console.ReadLine();

                    opcaoEscolhida = "5";
                    
                }

                else if (opcaoEscolhida == "0")
                {
                    Console.Clear();
                }

                else //Se não for a opção, aparece a mensagem
                {
                    opcaoEscolhida = "5";
                    Console.WriteLine();
                    Console.WriteLine("\nOpção digitada inválida.");
                    Console.ReadLine();
                }
            }


        }

        public static void CadastrarCategoria(Categoria cadCategoria)
        {

            Console.WriteLine();
            Console.Write("\nDigite o nome da Categoria: ");
            string nome = Console.ReadLine();

            try
            {
                if (nome == cadCategoria.Nome)
                {
                    throw new NomeExceptions("Já existe uma Categoria cadastrada com o mesmo nome.");
                }
            }

            catch(NomeExceptions e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário

            }

            
            cadCategoria.Nome = nome;
           
            try
            {
                cadCategoria.VerificarNome(cadCategoria);

            }

            catch (NomeExceptions e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
            }

            cadCategoria.DataCriacao = DateTime.Now;
            Console.Write("\nData da Criação: " + cadCategoria.DataCriacao);
            Console.Write("\nData da Modificação: Não há alterações.");

            cadCategoria.Status = true;

            if (cadCategoria.Status == true)

            {
                Console.WriteLine("\nStatus: Ativo");
            }

            else

            {
                Console.Write("\nStatus: Inativo");
            }

            Console.ReadLine();
            Console.WriteLine("Categoria cadastrada com sucesso!");
            Console.WriteLine();
            Console.WriteLine("Tecle ENTER para retornar ao MENU.");
            Console.ReadLine();

            
        }

        public static void CadastrarSub(SubCategoria cadSub, Categoria cadCategoria)
        {
            Console.WriteLine();
            Console.Write("\nCategoria: " + cadCategoria.Nome);
            Console.Write("\nDigite o nome da SubCategoria: ");
            string nome = Console.ReadLine();

            try
            {
                if (nome == cadSub.NomeSub || nome == cadCategoria.Nome)
                {
                    throw new NomeExceptions("Já existe uma Categoria/SubCategoria cadastrada com o mesmo nome.");
                }
            }

            catch (NomeExceptions e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário

            }

            cadSub.NomeSub = nome;

            try
            {
                cadSub.VerificarNome(cadSub);

            }

            catch (NomeExceptions e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
            }

            cadSub.DataCriacaoSub = DateTime.Now;
            Console.Write("\nData da Criação: " + cadSub.DataCriacaoSub);
            Console.Write("\nData da Modificação: Não há alterações.");

            cadSub.StatusSub = true;
            if (cadSub.StatusSub == true)

            {
                Console.WriteLine("\nStatus: Ativo");
            }

            else

            {
                Console.Write("\nStatus: Inativo");
            }

            Console.ReadLine();
            Console.WriteLine("SubCategoria cadastrada com sucesso!");
            Console.WriteLine();
            Console.WriteLine("Tecle ENTER para retornar ao MENU.");
            Console.ReadLine();
        }

        public static void EditarCategoria(Categoria cadCategoria, Categoria editCat)
        {

            string opcaoEditar = "4";

            Console.WriteLine();
            Console.WriteLine("Escolha a opção desejada: ");
            Console.WriteLine();
            Console.WriteLine("1 - Editar somente o nome da Categoria.");
            Console.WriteLine("2 - Editar somente o status da Categoria.");
            Console.WriteLine("3 - Editar o nome e o status da Categoria.");
            Console.WriteLine();

            Console.Write("Digite a opção escolhida: ");
            opcaoEditar = Console.ReadLine();

            if (opcaoEditar == "1")

            {
                Console.Write("\nEdite o nome da Categoria: " + cadCategoria.Nome);
                Console.WriteLine();
                Console.Write("\nDigite o nome da Categoria: ");
                cadCategoria.Nome = Console.ReadLine();

                try
                {
                    cadCategoria.VerificarNome(cadCategoria);

                }

                catch (NomeExceptions e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
                }

                cadCategoria.Status = true;

                if (cadCategoria.Status == true)

                {
                    Console.WriteLine("Status: Ativo");
                }

                else

                {
                    Console.Write("Status: Inativo");
                }

                Console.Write("Data da Criação: " + cadCategoria.DataCriacao);
                editCat.DataAlteracao = DateTime.Now;
                Console.Write("\nData da Alteração: " + editCat.DataAlteracao);
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("Categoria editada com sucesso!");
                Console.WriteLine();
                Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                Console.ReadLine();

            }

            else if (opcaoEditar == "2")

            {
                Console.Write("\nEdite o nome da Categoria: " + cadCategoria.Nome);
                Console.WriteLine();

                string opcaoStatus = "3";

                Console.Write("Digite 1 para Ativo ou 2 para Inativo: ");
                opcaoStatus = Console.ReadLine();

                if (opcaoStatus == "1")
                {
                    editCat.Status = true;
                    Console.WriteLine("Status: Ativo");
                    Console.ReadLine();
                }

                else if (opcaoStatus == "2")
                {
                    editCat.Status = false;
                    Console.WriteLine("Status: Inativo");
                    Console.ReadLine();
                }

                Console.Write("Data da Criação: " + cadCategoria.DataCriacao);
                editCat.DataAlteracao = DateTime.Now;
                Console.Write("\nData da Alteração: " + editCat.DataAlteracao);
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("Categoria editada com sucesso!");
                Console.WriteLine();
                Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                Console.ReadLine();

            }

            else if (opcaoEditar == "3")

            {
                Console.Write("\nEdite o nome da Categoria: " + cadCategoria.Nome);
                Console.WriteLine();
                Console.Write("\nDigite o nome da Categoria: ");
                cadCategoria.Nome = Console.ReadLine();

                try
                {
                    cadCategoria.VerificarNome(cadCategoria);

                }

                catch (NomeExceptions e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
                }

                string opcaoStatus = "3";

                Console.Write("Digite 1 para Ativo ou 2 para Inativo: ");
                opcaoStatus = Console.ReadLine();

                if (opcaoStatus == "1")
                {
                    editCat.Status = true;
                    Console.WriteLine("Status: Ativo");
                    Console.ReadLine();
                }

                else if (opcaoStatus == "2")
                {
                    editCat.Status = false;
                    Console.WriteLine("Status: Inativo");
                    Console.ReadLine();
                }

                Console.Write("Data da Criação: " + cadCategoria.DataCriacao);
                editCat.DataAlteracao = DateTime.Now;
                Console.Write("\nData da Alteração: " + editCat.DataAlteracao);
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("Categoria editada com sucesso!");
                Console.WriteLine();
                Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                Console.ReadLine();
            }
        }

        public static void EditarSub(SubCategoria cadSub, SubCategoria editSub)
        {

            string opcaoEditar = "4";

            Console.WriteLine();
            Console.WriteLine("Escolha a opção desejada: ");
            Console.WriteLine();
            Console.WriteLine("1 - Editar somente o nome da SubCategoria.");
            Console.WriteLine("2 - Editar somente o status da SubCategoria.");
            Console.WriteLine("3 - Editar o nome e o status da SubCategoria.");
            Console.WriteLine();

            Console.Write("Digite a opção escolhida: ");
            opcaoEditar = Console.ReadLine();

            if (opcaoEditar == "1")

            {
                Console.Write("\nEdite o nome da SubCategoria: " + cadSub.NomeSub);
                Console.WriteLine();
                Console.Write("\nDigite o nome da Categoria: ");
                cadSub.NomeSub = Console.ReadLine();

                try
                {
                    cadSub.VerificarNome(cadSub);

                }

                catch (NomeExceptions e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
                }

                cadSub.StatusSub = true;

                if (cadSub.StatusSub == true)

                {
                    Console.WriteLine("Status: Ativo");
                }

                else

                {
                    Console.Write("Status: Inativo");
                }

                Console.Write("Data da Criação: " + cadSub.DataCriacaoSub);
                editSub.DataAlteracaoSub = DateTime.Now;
                Console.Write("\nData da Alteração: " + editSub.DataAlteracaoSub);
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("SubCategoria editada com sucesso!");
                Console.WriteLine();
                Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                Console.ReadLine();

            }

            else if (opcaoEditar == "2")

            {
                Console.Write("\nEdite o nome da Categoria: " + cadSub.NomeSub);
                Console.WriteLine();

                string opcaoStatus = "3";

                Console.Write("Digite 1 para Ativo ou 2 para Inativo: ");
                opcaoStatus = Console.ReadLine();

                if (opcaoStatus == "1")
                {
                    editSub.StatusSub = true;
                    Console.WriteLine("Status: Ativo");
                    Console.ReadLine();
                }

                else if (opcaoStatus == "2")
                {
                    editSub.StatusSub = false;
                    Console.WriteLine("Status: Inativo");
                    Console.ReadLine();
                }

                Console.Write("Data da Criação: " + cadSub.DataCriacaoSub);
                editSub.DataAlteracaoSub = DateTime.Now;
                Console.Write("\nData da Alteração: " + editSub.DataAlteracaoSub);
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("SubCategoria editada com sucesso!");
                Console.WriteLine();
                Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                Console.ReadLine();

            }

            else if (opcaoEditar == "3")

            {
                Console.Write("\nEdite o nome da Categoria: " + cadSub.NomeSub);
                Console.WriteLine();
                Console.Write("\nDigite o nome da Categoria: ");
                cadSub.NomeSub = Console.ReadLine();

                try
                {
                    cadSub.VerificarNome(cadSub);

                }

                catch (NomeExceptions e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
                }

                string opcaoStatus = "3";

                Console.Write("Digite 1 para Ativo ou 2 para Inativo: ");
                opcaoStatus = Console.ReadLine();

                if (opcaoStatus == "1")
                {
                    editSub.StatusSub = true;
                    Console.WriteLine("Status: Ativo");
                    Console.ReadLine();
                }

                else if (opcaoStatus == "2")
                {
                    editSub.StatusSub = false;
                    Console.WriteLine("Status: Inativo");
                    Console.ReadLine();
                }

                Console.Write("Data da Criação: " + cadSub.DataCriacaoSub);
                editSub.DataAlteracaoSub = DateTime.Now;
                Console.Write("\nData da Alteração: " + editSub.DataAlteracaoSub);
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("SubCategoria editada com sucesso!");
                Console.WriteLine();
                Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                Console.ReadLine();
            }
        }


    }
}
