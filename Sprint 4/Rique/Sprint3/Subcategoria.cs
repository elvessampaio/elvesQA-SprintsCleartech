using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint3
{
    public class Subcategoria : Categoria
    {
        //CADASTRA UMA SUBCATEGORIA
        public void CadastrarSubcategoria(Categoria categoria)
        {
            Console.Clear();
            Console.WriteLine("Vamos criar uma subcategoria para a categoria " + categoria.Nome + ".\n\n");
            Console.Write("Defina um nome para a subcategoria: ");
            NomeParaVerificar = Console.ReadLine();
            try
            {
                VerificarMesmoNomeCategoria(categoria);
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

            Console.WriteLine("\nSubcategoria criada com sucesso!\n");
            Console.WriteLine("Nome da subcategoria: " + Nome);

            string mostrasts;
            if (Status == true)
                mostrasts = "Ativa.";
            else
                mostrasts = "Inativa.";

            Console.WriteLine("Status da subcategoria: " + mostrasts);
            Console.WriteLine("Data da criação da subcategoria: " + DataEHoraCriacao);
            Console.WriteLine("Data da modificação da subcategoria: Não houve modificação.\n");
            Console.WriteLine(Nome + " pertence à categoria " + categoria.Nome + ".");

            Console.WriteLine("\nTecle Enter para continuar.");
            Console.ReadLine();
        }

        //EDITA A SUBCATEGORIA
        public void EditarSubcategoria(Categoria categoria)
        {
            Console.Clear();
            Console.WriteLine("Vamos editar a subcategoria " + Nome + ".\n");
            Console.Write("Deseja editar o Nome e Status [1], apenas o Nome [2] ou apenas o Status [3]? ");
            string opcao = Console.ReadLine();

            //Edita Nome e Status da Categoria
            if (opcao == "1")
            {
                EditarNome(categoria);
                EditarStatus();
            }

            //Edita apenas o Nome da Categoria
            else if (opcao == "2")
            {
                EditarNome(categoria);
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

            Console.WriteLine("\nSubcategoria editada com sucesso.");
            Console.WriteLine("\nNovo nome da subcategoria: " + Nome);
            Console.WriteLine("Novo status da subcategoria: " + mostrasts);
            Console.WriteLine("Data da criação da subcategoria: " + DataEHoraCriacao);
            Console.WriteLine("Data da modificação da subcategoria: " + DataEHoraModificacao);
            Console.WriteLine(Nome + " pertence à categoria " + categoria.Nome + ".");

            Console.WriteLine("\nTecle Enter para continuar.");
            Console.ReadLine();
        }

        public void VerificarMesmoNomeSubcategoria(Categoria categoria) // Verifica se o Nome da Categoria não é igual ao da Subcategoria (quando for editar a categoria)
        {
            if(cadastro == true && categoria.NomeParaVerificar == Nome)
            {
                throw new NomeException("\nAtenção: o nome da categoria não pode ser o mesmo que o da subcategoria.");
            }
        }

        public void VerificarMesmoNomeCategoria(Categoria categoria) // Verifica se o Nome da Subcategoria não é igual ao da Categoria
        {
            if(NomeParaVerificar == categoria.Nome)
            {
                throw new NomeException("\nAtenção: o nome da subcategoria não pode ser o mesmo que o da categoria.");
            }
        } 

        private void EditarNome(Categoria categoria) // Edita o Nome da Subcategoria
        {
            Console.Write("\nEscreva um novo nome para a subcategoria: ");
            NomeParaVerificar = Console.ReadLine();
            try
            {
                VerificarMesmoNome();
                VerificarMesmoNomeCategoria(categoria);
                VerificarNome();
            }
            catch (NomeException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Ocorreu uma exceção. Retornando ao Menu Principal.");
                throw;
            }
        }

        private void EditarStatus() //Edita o Status da Subcategoria
        {
            Console.Write("\nDefina um status para a subcategoria: [1] Ativa ou [2] Inativa  ");

            //Status Subcategoria
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

    }
}
