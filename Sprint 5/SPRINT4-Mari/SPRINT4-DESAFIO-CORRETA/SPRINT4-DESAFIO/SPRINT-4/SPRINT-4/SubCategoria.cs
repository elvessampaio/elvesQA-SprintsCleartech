using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Categorias;
using NomeException;

namespace SPRINT_4
{
    public class SubCategoria

    {
        public List<SubCategoria> sublist = new List<SubCategoria>();

        //PROPRIEDADES
        public string Nome { get; private set; } //não é para ser alterado no program
        public bool Status { get; private set; }
        public System.DateTime DataCriacao { get; private set; }
        public System.DateTime DataAlteracao { get; private set; }
        public System.DateTime DataPadrao { get; private set; }

        public string CategoriaID;

        public bool autenticar;


        public SubCategoria(string nome, bool status, DateTime dataCriacao, DateTime dataAlteracao, string categoriaID)
        {
            Nome = nome;
            Status = status;
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;
            CategoriaID = categoriaID;

        }

        public SubCategoria()
        {
        }


        public void CadastrarSub(SubCategoria cadSub, List<Categoria> catlist)

        {
            Console.WriteLine();
            Console.Write("\nDigite o nome da Categoria para cadastrar a SubCategoria: ");
            string nomeCad;
            nomeCad = Console.ReadLine();

            Categoria novaCategoria = catlist.Find(c => c.Nome == nomeCad);

            if (novaCategoria != null)
            {
                Console.Write("\nDigite o nome da SubCategoria: ");
                string nome = Console.ReadLine();

                if (nome == cadSub.Nome)
                {
                    throw new NomeExceptions("Já existe uma SubCategoria cadastrada com o mesmo nome.");
                }

                cadSub.Nome = nome;

                try

                {
                    VerificarNome(cadSub); //Vai tentar cadastrar e verificar se há alguma exceção
                }

                catch (NomeExceptions e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
                }

                cadSub.DataCriacao = DateTime.Now;
                Console.Write("\nData da Criação: " + cadSub.DataCriacao);

                Console.WriteLine("\nData da Modificação: Não há alterações.");


                cadSub.Status = true;

                if (cadSub.Status == true)

                {
                    Console.WriteLine("Status: Ativo");
                }

                else

                {
                    Console.Write("Status: Inativo");
                }



                cadSub.CategoriaID = novaCategoria.Nome;


                var novaSub = new SubCategoria(cadSub.Nome, cadSub.Status, cadSub.DataCriacao, cadSub.DataAlteracao, cadSub.CategoriaID);
                sublist.Add(novaSub);

                Console.WriteLine("Subcategoria cadastrada com sucesso!");
                Console.WriteLine();
                Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                Console.ReadLine();


            }

            else

            {
                throw new NomeExceptions("Categoria digitada não foi encontrada. Tente novamente.");
            }



        }

        public void EditarSub(List<SubCategoria> sublist)
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
            opcaoEditar = Console.ReadKey().KeyChar.ToString();


            if (opcaoEditar == "1")
            {
                Console.Write("\nDigite o nome da SubCategoria: ");
                string nome;
                nome = Console.ReadLine();

                SubCategoria novaSub = sublist.Find(s => s.Nome == nome); //exp. lambda com o argumento c para retornar o valor da pesquisa

                if (novaSub != null)
                {
                    Console.Write("\nDigite o novo nome da SubCategoria: ");
                    string novoNome = Console.ReadLine();
                    novaSub.Nome = novoNome; //vai guardar o novo nome na lista

                    try
                    {
                        VerificarNome(novaSub); //Vai tentar cadastrar e verificar se há alguma exceção
                    }

                    catch (NomeExceptions e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
                    }


                    novaSub.Status = true;

                    if (novaSub.Status == true)

                    {
                        Console.Write("\nStatus: Ativo");
                    }

                    else

                    {
                        Console.Write("\nStatus: Inativo");
                    }

                    Console.Write("\nData da Criação: " + novaSub.DataCriacao);
                    novaSub.DataAlteracao = DateTime.Now;
                    Console.Write("\nData da Alteração: " + novaSub.DataAlteracao);
                    Console.Write("\nPertence a Categoria: " + novaSub.CategoriaID);
                    Console.WriteLine();
                    Console.Write("\nSubCategoria editada com sucesso!");
                    Console.WriteLine();
                    Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                    Console.ReadLine();
                }

                else

                {
                    throw new NomeExceptions("SubCategoria digitada não foi encontrada. Tente novamente.");
                }


            }

            else if (opcaoEditar == "2")

            {
                Console.Write("\nDigite o nome da SubCategoria: ");
                string nome;
                nome = Console.ReadLine();

                SubCategoria novaSub = sublist.Find(s => s.Nome == nome); //exp. lambda com o argumento c para retornar o valor da pesquisa

                if (novaSub != null)
                {
                    Console.Write("\nSelecione o novo status da Categoria: ");
                    string opcaoStatus = "3";

                    Console.Write("\nDigite 1 para Ativo ou 2 para Inativo: ");
                    opcaoStatus = Console.ReadLine();

                    bool novoStatus = true;

                    if (opcaoStatus == "1")
                    {
                        novoStatus = true;
                        Console.WriteLine("\nStatus: Ativo");

                    }

                    else if (opcaoStatus == "2")
                    {
                        novoStatus = false;
                        Console.WriteLine("\nStatus: Inativo");

                    }

                    novaSub.Status = novoStatus;

                    Console.Write("\nData da Criação: " + novaSub.DataCriacao);
                    novaSub.DataAlteracao = DateTime.Now;
                    Console.Write("\nData da Alteração: " + novaSub.DataAlteracao);
                    Console.Write("\nPertence a Categoria: " + novaSub.CategoriaID);
                    Console.WriteLine();
                    Console.Write("\nSubCategoria editada com sucesso!");
                    Console.WriteLine();
                    Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                    Console.ReadLine();
                }

                else

                {
                    throw new NomeExceptions("SubCategoria digitada não foi encontrada. Tente novamente.");
                }

            }



            else if (opcaoEditar == "3")
            {

                Console.Write("\nDigite o nome da SubCategoria: ");
                string nome;
                nome = Console.ReadLine();

                SubCategoria novaSub = sublist.Find(s => s.Nome == nome); //exp. lambda com o argumento c para retornar o valor da pesquisa

                if (novaSub != null)
                {
                    Console.Write("\nDigite o novo nome da SubCategoria: ");
                    string novoNome = Console.ReadLine();
                    novaSub.Nome = novoNome; //vai guardar o novo nome na lista

                    try
                    {
                        VerificarNome(novaSub); //Vai tentar cadastrar e verificar se há alguma exceção
                    }

                    catch (NomeExceptions e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
                    }

                    Console.Write("\nSelecione o novo status da Categoria: ");
                    string opcaoStatus = "3";

                    Console.Write("\nDigite 1 para Ativo ou 2 para Inativo: ");
                    opcaoStatus = Console.ReadLine();

                    bool novoStatus = true;

                    if (opcaoStatus == "1")
                    {
                        novoStatus = true;
                        Console.WriteLine("\nStatus: Ativo");

                    }

                    else if (opcaoStatus == "2")
                    {
                        novoStatus = false;
                        Console.WriteLine("\nStatus: Inativo");

                    }

                    novaSub.Status = novoStatus;

                    Console.Write("\nData da Criação: " + novaSub.DataCriacao);
                    novaSub.DataAlteracao = DateTime.Now;
                    Console.Write("\nData da Alteração: " + novaSub.DataAlteracao);
                    Console.Write("\nPertence a Categoria: " + novaSub.CategoriaID);
                    Console.WriteLine();
                    Console.Write("\nSubCategoria editada com sucesso!");
                    Console.WriteLine();
                    Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                    Console.ReadLine();


                }

                else

                {
                    throw new NomeExceptions("SubCategoria digitada não foi encontrada. Tente novamente.");
                }

            }

            else
            {
                opcaoEditar = "4";
                Console.WriteLine();
                Console.WriteLine("Digite a opção correta.");
                Console.ReadLine();
            }
        }

        public void ConsultarSub(List<SubCategoria> sublist)
        {
            Console.WriteLine();
            Console.Write("\nDigite o nome da SubCategoria: ");
            string nome;
            nome = Console.ReadLine();

            if (nome.Length > 128)
            {
                Console.WriteLine("O campo permite apenas 128 caracteres.");
            }

            SubCategoria novaSub = sublist.Find(s => s.Nome == nome);

            if (novaSub != null)
            {
                Console.Write("\nNome da SubCategoria: " + novaSub.Nome);
                Console.Write("\nPertence a Categoria: " + novaSub.CategoriaID);

                if (novaSub.Status == true)

                {
                    Console.Write("\nStatus: Ativo");
                }

                else

                {
                    Console.Write("\nStatus: Inativo");
                }

                Console.Write("\nData da Criação: " + novaSub.DataCriacao);


                if (novaSub.DataPadrao == novaSub.DataAlteracao)
                {
                    Console.Write("\nData da Modificação: Não há alterações.");
                }

                else
                {
                    Console.Write("\nData da Modificação: " + novaSub.DataAlteracao);
                }

                Console.WriteLine();
                Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                Console.ReadLine();
            }

            else
            {
                throw new NomeExceptions("SubCategoria digitada não foi encontrada. Tente novamente.");
            }




        }

        public static void VerificarNome(SubCategoria verificar)
        {
            if (string.IsNullOrEmpty(verificar.Nome))

            {
                throw new NomeExceptions("O nome da categoria/subcategoria não pode ser vazia!");
            }

            if (verificar.Nome.Length > 128)
            {
                throw new NomeExceptions("O nome da categoria/subcategoria deve conter apenas 128 caracteres.");
            }

            if (!Regex.IsMatch(verificar.Nome, "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$")) //é usado para validar uma cadeia de caracteres ou para garantir que uma cadeia de caracteres esteja em conformidade
            {
                throw new NomeExceptions("O nome da categoria/subcategoria deve conter somente letras do alfabeto.");
            }

            if (Regex.IsMatch(verificar.Nome, "^[ ]*$")) //para espaços vazios
            {
                throw new NomeExceptions("O nome da categoria/subcategoria não pode ser vazia!");
            }

        }

    }
}

