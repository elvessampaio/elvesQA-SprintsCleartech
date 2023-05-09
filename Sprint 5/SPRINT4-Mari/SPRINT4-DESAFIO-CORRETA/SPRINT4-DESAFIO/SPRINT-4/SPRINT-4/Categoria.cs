using NomeException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Categorias
{
    public class Categoria
    {
        //LISTA

        public List<Categoria> catlist = new List<Categoria>();

        //PROPRIEDADES
        public string Nome { get; private set; }
        public bool Status { get; private set; } //não é para ser alterado no program
        public System.DateTime DataCriacao { get; private set; }
        public System.DateTime DataAlteracao { get; private set; }
        public System.DateTime DataPadrao { get; private set; }//usado só para alterar a data de modificação padrão ao consultar a categoria

        public bool autenticar;

        public Categoria(string nome, bool status, DateTime dataCriacao, DateTime dataAlteracao)
        {
            Nome = nome;
            Status = status;
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;
        }

        public Categoria()
        {
        }


        //Método de Cadastrar Categoria
        public void CadastrarCategoria(Categoria cad)

        {
            Console.WriteLine();
            Console.Write("\nDigite o nome da Categoria: ");
            string nome = Console.ReadLine();

            if (nome == cad.Nome)
            {
                throw new NomeExceptions("Já existe uma Categoria cadastrada com o mesmo nome.");
            }

            cad.Nome = nome;

            try
            {
                VerificarNome(cad); //Vai tentar cadastrar e verificar se há alguma exceção
            }

            catch (NomeExceptions e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
            }


            cad.DataCriacao = DateTime.Now;
            Console.Write("\nData da Criação: " + cad.DataCriacao);

            cad.DataAlteracao = cad.DataPadrao;

            Console.WriteLine("\nData da Modificação: Não há alterações.");


            cad.Status = true;

            if (cad.Status == true)

            {
                Console.WriteLine("Status: Ativo");
            }

            else

            {
                Console.Write("Status: Inativo");
            }

            var novaCategoria = new Categoria(cad.Nome, cad.Status, cad.DataCriacao, cad.DataAlteracao);
            catlist.Add(novaCategoria);

            Console.WriteLine("Categoria cadastrada com sucesso!");
            Console.WriteLine();
            Console.WriteLine("Tecle ENTER para retornar ao MENU.");
            Console.ReadLine();
        }

        public void EditarCategoria(List<Categoria> catlist)
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
            opcaoEditar = Console.ReadKey().KeyChar.ToString();

            //Editar somente o nome

            if (opcaoEditar == "1")
            {

                Console.WriteLine();
                Console.Write("\nDigite o nome da Categoria: ");
                string nome;
                nome = Console.ReadLine();


                Categoria novaCategoria = catlist.Find(c => c.Nome == nome); //exp. lambda com o argumento c para retornar o valor da pesquisa

                if (novaCategoria != null)
                {
                    Console.Write("\nDigite o novo nome da Categoria: ");
                    string novoNome = Console.ReadLine();
                    novaCategoria.Nome = novoNome; //vai guardar o novo nome na lista

                    try
                    {
                        VerificarNome(novaCategoria); //Vai tentar cadastrar e verificar se há alguma exceção
                    }

                    catch (NomeExceptions e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                        throw; //o throw é para ele parar o programa quando encontra a exceção digitada pelo usuário
                    }


                    novaCategoria.Status = true;

                    if (novaCategoria.Status == true)

                    {
                        Console.Write("\nStatus: Ativo");
                    }

                    else

                    {
                        Console.Write("\nStatus: Inativo");
                    }

                    Console.Write("\nData da Criação: " + novaCategoria.DataCriacao);
                    novaCategoria.DataAlteracao = DateTime.Now;
                    Console.Write("\nData da Alteração: " + novaCategoria.DataAlteracao);
                    Console.WriteLine();
                    Console.Write("\nCategoria editada com sucesso!");
                    Console.WriteLine();
                    Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                    Console.ReadLine();


                }

                else

                {
                    throw new NomeExceptions("Categoria digitada não foi encontrada. Tente novamente.");
                }

            }

            //Editar somente o status

            else if (opcaoEditar == "2")
            {
                Console.WriteLine();
                Console.Write("\nDigite o nome da Categoria: ");
                string nome;
                nome = Console.ReadLine();

                Categoria novaCategoria = catlist.Find(c => c.Nome == nome);

                if (novaCategoria != null)
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

                    novaCategoria.Status = novoStatus;

                    Console.Write("\nData da Criação: " + novaCategoria.DataCriacao);
                    novaCategoria.DataAlteracao = DateTime.Now;
                    Console.Write("\nData da Alteração: " + novaCategoria.DataAlteracao);
                    Console.WriteLine();
                    Console.Write("\nCategoria editada com sucesso!");
                    Console.WriteLine();
                    Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                    Console.ReadLine();
                }

                else

                {
                    throw new NomeExceptions("Categoria digitada não foi encontrada. Tente novamente.");
                }


            }

            //Editar os dois (nome e status)

            else if (opcaoEditar == "3")
            {
                Console.WriteLine();
                Console.Write("\nDigite o nome da Categoria: ");
                string nome;
                nome = Console.ReadLine();

                Categoria novaCategoria = catlist.Find(c => c.Nome == nome);

                if (novaCategoria != null)
                {
                    Console.Write("\nDigite o novo nome da Categoria: ");
                    string novoNome = Console.ReadLine();

                    novaCategoria.Nome = novoNome;

                    try
                    {
                        VerificarNome(novaCategoria); //Vai tentar cadastrar e verificar se há alguma exceção
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

                    novaCategoria.Status = novoStatus;

                    Console.Write("\nData da Criação: " + novaCategoria.DataCriacao);
                    novaCategoria.DataAlteracao = DateTime.Now;
                    Console.Write("\nData da Alteração: " + novaCategoria.DataAlteracao);
                    Console.WriteLine();
                    Console.Write("\nCategoria editada com sucesso!");
                    Console.WriteLine();
                    Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                    Console.ReadLine();
                }

                else

                {
                    throw new NomeExceptions("Categoria digitada não foi encontrada. Tente novamente.");
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

        public void ConsultarCategoria(List<Categoria> catlist)
        {


            Console.WriteLine();
            Console.Write("\nDigite o nome da Categoria: ");
            string nome;
            nome = Console.ReadLine();

            if (nome.Length > 128)
            {
                Console.WriteLine("O campo permite apenas 128 caracteres.");
            }

            Categoria novaCategoria = catlist.Find(c => c.Nome == nome);
            {
                if (novaCategoria != null)
                {
                    Console.Write("\nNome da categoria: " + novaCategoria.Nome);
                    if (novaCategoria.Status == true)

                    {
                        Console.Write("\nStatus: Ativo");
                    }

                    else

                    {
                        Console.Write("\nStatus: Inativo");
                    }
                    Console.Write("\nData da Criação: " + novaCategoria.DataCriacao);


                    if (novaCategoria.DataPadrao == novaCategoria.DataAlteracao)
                    {
                        Console.Write("\nData da Modificação: Não há alterações.");
                    }

                    else
                    {
                        Console.Write("\nData da Modificação: " + novaCategoria.DataAlteracao);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Tecle ENTER para retornar ao MENU.");
                    Console.ReadLine();
                }

                else
                {
                    throw new NomeExceptions("Categoria digitada não foi encontrada. Tente novamente.");
                }
            }



        }

        //PARTE DAS EXCEÇÕES DA CATEGORIA
        public static void VerificarNome(Categoria verificar)
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
