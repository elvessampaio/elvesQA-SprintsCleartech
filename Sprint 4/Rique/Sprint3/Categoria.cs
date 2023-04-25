using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint3
{
    public class Categoria
    {
        public bool cadastro = false;
        public string NomeParaVerificar { get; set; } // Propriedade usada para verificar se o nome digitado pelo usuário atende aos critérios de aceite.
        public string Nome { get; set; } // Receberá o nome da categoria escolhida pelo usuário.
        public bool Status { get; set; } // Receberá o status da categora, inicialmente setada como true.
        public System.DateTime DataEHoraCriacao { get; set; } // Receberá, no formato data e hora, a função DateTime no momento que for criada uma categoria.
        public System.DateTime DataEHoraModificacao { get; set; } // Receberá a função DateTime quando a categoria for modificada.
        public System.DateTime valorPadrao { get; }

        readonly int maxDeCaracteres = 128;

        //CADASTRA UMA CATEGORIA
        public void CadastrarCategoria()
        {
            Console.Clear();
            Console.WriteLine("Vamos criar uma nova categoria!\n\n");
            Console.Write("Defina um nome para a categoria: ");
            NomeParaVerificar = Console.ReadLine();
            try
            {
                VerificarNome();
            }
            catch (NomeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Ocorreu uma exceção. Retornando ao Menu Principal.");
                throw;
            }

            Status = true;
            DataEHoraCriacao = DateTime.Now;
            DataEHoraModificacao = valorPadrao;

            Console.WriteLine("\nCategoria criada com sucesso!\n");
            Console.WriteLine("Nome da categoria: " + Nome);

            string mostrasts;
            if (Status == true)
                mostrasts = "Ativa.";
            else
                mostrasts = "Inativa.";

            Console.WriteLine("Status da categoria: " + mostrasts);
            Console.WriteLine("Data da criação: " + DataEHoraCriacao);
            Console.WriteLine("Data da modificação: Não houve modificação.\n");

            Console.WriteLine("Tecle Enter para continuar.");
            Console.ReadLine();
        }

        //EDITA A CATEGORIA
        public void EditarCategoria(Subcategoria subcategoria)
        {
            Console.Clear();
            Console.WriteLine("Vamos editar a categoria " + Nome + ".\n");
            Console.Write("Deseja editar o Nome e Status [1], apenas o Nome [2] ou apenas o Status [3]? ");
            string opcao = Console.ReadLine();

            //Edita Nome e Status da Categoria
            if (opcao == "1")
            {
                EditarNome(subcategoria);
                EditarStatus();
            }

            //Edita apenas o Nome da Categoria
            else if (opcao == "2")
            {
                EditarNome(subcategoria);
            }

            //Edita apenas o Status da Categoria
            else if (opcao == "3")
            {
                EditarStatus();
            }

            // Quando uma opção inválida é digitada, retorna ao menu principal.
            else
            {
                Console.WriteLine("Opção inválida. Retornando ao menu principal.");
                return;
            }

            string mostrasts;
            if (Status == true)
                mostrasts = "Ativa.";
            else
                mostrasts = "Inativa.";

            DataEHoraModificacao = DateTime.Now;


            Console.WriteLine("\nCategoria editada com sucesso.");
            Console.WriteLine("\nNovo nome da categoria: " + Nome);
            Console.WriteLine("Novo status da categoria: " + mostrasts);
            Console.WriteLine("Data da criação: " + DataEHoraCriacao);
            Console.WriteLine("Data da modificação: " + DataEHoraModificacao);

            Console.WriteLine("\nTecle Enter para continuar.");
            Console.ReadLine();
        }

        private void EditarNome(Subcategoria subcategoria) // Edita o Nome da Categoria.
        {
            Console.Write("\nEscreva um novo nome para a categoria: ");
            NomeParaVerificar = Console.ReadLine();
            try
            {
                VerificarMesmoNome();
                subcategoria.VerificarMesmoNomeSubcategoria(this);
                VerificarNome();
            }
            catch (NomeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Ocorreu uma exceção. Retornando ao Menu Principal.");
                throw;
            }
        }

        private void EditarStatus() // Edita o Status da Categoria.
        {
            Console.Write("\nDefina um status para a categoria: [1] Ativa ou [2] Inativa  ");

            bool verificacaoStatus = true;
            while (verificacaoStatus == true)
            {
                var ler = Console.ReadLine();
                if (ler == "1")
                {
                    Status = true;
                    verificacaoStatus = false;
                }
                else if (ler == "2")
                {
                    Status = false;
                    verificacaoStatus = false;
                }
                else
                    Console.Write("\nOpção inválida. Tente novamente: [1] Ativa ou [2] Inativa   ");
            }
        }

        public void VerificarMesmoNome() // Verifica se o nome cadastrado é igual ao cadastrado anteriormente.
        {
            if (Nome == NomeParaVerificar)
            {
                throw new NomeException("\nAtenção: O nome digitado é o mesmo cadastrado anteriormente.");
            }
        }

        private void VerificarNomeVazio() // Verifica se o campo Nome está vazio.
        {
            if (NomeParaVerificar == "")
            {
                throw new NomeException("\nAtenção: o campo 'Nome' não pode ficar vazio.");
            }
        }

        private void VerificarTamanhoDoNome() // Verifica se o tamanho dgitado está dentro do limite esperado.
        {
            if (NomeParaVerificar.Length > maxDeCaracteres)
            {
                throw new NomeException("\nAtenção: o tamanho máximo permitido é de " + maxDeCaracteres + " caracteres.");
            }
        }

        private void VerificarAlfabetoNoNome() // Verifica se o nome possui apenas letras em sua composição.
        {
            string testarAlfabeto = NomeParaVerificar.Replace(" ", "");
            if (!testarAlfabeto.All(char.IsLetter)) //! é operador NOT. Verifica caractere por caractere, em toda string em Nome se char.IsLetter (se existe letra) é verdadeiro. No início tem um ! que significa que o if será verdadeiro quando a condição não for verdadeira.
            {
                throw new NomeException("\nAtenção: Nome inválido. Não é permitido criar um nome com caracteres especiais.");
            }
        }

        public void VerificarNome() // Método que reúne as verificações mais usuais.
        {
            VerificarNomeVazio();
            VerificarTamanhoDoNome();
            VerificarAlfabetoNoNome();
            Nome = NomeParaVerificar;
        }

        public void Apagar() // Método que apaga os valores salvos.
        {
            Nome = "";
            Status = false;
            DataEHoraCriacao = valorPadrao;
            DataEHoraModificacao = valorPadrao;
            cadastro = false;
        }

    }
}
