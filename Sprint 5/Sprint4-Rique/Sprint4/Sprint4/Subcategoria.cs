using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4
{
    public class Subcategoria : Categoria
    {
        public Guid CodigoID { get; private set; }
        public Subcategoria(string nome, bool status, DateTime criacao, DateTime modificacao, Guid codigoID)
        {
            Nome = nome;
            Status = status;
            DataEHoraCriacao = criacao;
            DataEHoraModificacao = modificacao;
            CodigoID = codigoID;
        }

        //CADASTRAR SUBCATEGORIA
        public static void Cadastrar(List<Subcategoria> listaS, List<Categoria> listaC)
        {
            Console.Clear();
            Console.WriteLine("CRIAR SUBCATEGORIA!\n\n");

            Console.WriteLine("Pesquise à qual categoria deseja alocar a nova subcategoria.");

            var indiceC = PesquisarCatPeloNome(listaC);

            if (indiceC == -1)
            {
                throw new NomeException("Atenção: A categoria não existe.");
            }

            if (listaC[indiceC].Status == false)
            {
                throw new StatusException($"\nAtenção: A categoria {listaC[indiceC].Nome} está Inativa, " +
                    $"portanto não pode receber novos cadastros de subcategoria.\nAtive-a para habilitar o cadastro " +
                    $"de novas subcategorias.");
            }

            Console.WriteLine($"\nVamos criar uma subcategoria em {listaC[indiceC].Nome}.");
            Console.Write("Defina um nome para a subcategoria: ");
            string nome = Console.ReadLine();

            try
            {
                nome = VerificarNome(nome, indiceC, listaC, listaS);
            }
            catch (NomeException)
            {
                throw;
            }

            listaS.Add(new Subcategoria(nome, true, DateTime.Now, ValorPadrao, listaC[indiceC].ID));

            Console.WriteLine("\nSubcategoria criada com sucesso!\n");
            Console.WriteLine($"Nome da subcategoria: {listaS[listaS.Count - 1].Nome}.");

            string mostrasts;
            if (listaS[listaS.Count - 1].Status == true)
                mostrasts = "Ativo";
            else
                mostrasts = "Inativo";

            Console.WriteLine($"Status da categoria: {mostrasts}.");
            Console.WriteLine($"Data da criação: {listaS[listaS.Count - 1].DataEHoraCriacao}");
            Console.WriteLine("Data da modificação: Não houve modificação.\n");

            Console.WriteLine("Tecle Enter para retornar ao Menu Principal.");
            Console.ReadLine();
        }
        
        //EDITAR SUBCATEGORIA
        public static void Editar(List<Subcategoria> listaS, List<Categoria> listaC)
        {
            Console.Clear();
            Console.WriteLine("EDIÇÃO DE SUBCATEGORIA!");
            Console.WriteLine("\nPesquise a subcategoria que deseja editar.");
            int indiceS;
            try
            {
                indiceS = PesquisarSubPeloNome(listaS);
            }
            catch (NomeException)
            {
                throw;
            }
            var indiceC = PesquisarIndiceCat(listaS, listaC, indiceS);


            if(indiceS == -1)
            {
                Console.WriteLine("Atenção: A subcategoria não existe.");
                return;
            }

            if (listaC[indiceC].Status == false)
            {
                throw new StatusException($"\nAtenção: A subcategoria {listaS[indiceS].Nome} pertence à " +
                    $"categoria {listaC[indiceC].Nome}, que está Inativa, portanto não pode ser editada.\n" +
                    $"Ative-a para habilitar sua edição.");
            }

            Console.WriteLine($"\nVamos editar a subcategoria {listaS[indiceS].Nome}.");
            Console.Write("Deseja editar o Nome e Status [1], apenas o Nome [2] ou apenas o Status [3]? ");
            var opcao = Console.ReadLine();
            var novoNome = listaS[indiceS].Nome;
            var novoStatus = listaS[indiceS].Status;
            var id = listaC[indiceC].ID;

            //Edita Nome e Status da Subcategoria
            if (opcao == "1")
            {
                Console.Write("\nDigite um novo nome para a subcategoria: ");
                novoNome = Console.ReadLine();
                try
                {
                    novoNome = VerificarNome(novoNome, indiceS, listaC, listaS);
                }
                catch (NomeException)
                {
                    throw;
                }
                
                novoStatus = EditarStatus(novoStatus);
            }

            //Edita apenas o Nome da Subcategoria
            else if (opcao == "2")
            {
                Console.Write("\nDigite um novo nome para a subcategoria: ");
                novoNome = Console.ReadLine();
                try
                {
                    novoNome = VerificarNome(novoNome, indiceS, listaC, listaS);
                }
                catch (NomeException)
                {
                    throw;
                }
            }

            //Edita apenas o Status da Subcategoria
            else if (opcao == "3")
            {
                novoStatus = EditarStatus(novoStatus);
            }

            // Quando uma opção inválida é digitada, retorna ao menu principal.
            else
            {
                Console.WriteLine("Opção inválida. Retornando ao Menu Principal.");
                return;
            }

            DateTime criacao = listaS[indiceS].DataEHoraCriacao;

            listaS.Remove(listaS[indiceS]);
            listaS.Insert(indiceS, new Subcategoria(novoNome, novoStatus, criacao, DateTime.Now, id));

            string mostrasts;
            if (novoStatus == true)
                mostrasts = "Ativa.";
            else
                mostrasts = "Inativa.";

            Console.WriteLine("\nSubcategoria editada com sucesso.");
            Console.WriteLine("\nNovo nome da subcategoria: " + listaS[indiceS].Nome);
            Console.WriteLine("Novo status da subcategoria: " + mostrasts);
            Console.WriteLine("Data da criação: " + listaS[indiceS].DataEHoraCriacao);
            Console.WriteLine("Data da modificação: " + listaS[indiceS].DataEHoraModificacao);

            Console.WriteLine("\nTecle Enter para retornar ao Menu Principal.");
            Console.ReadLine();
        }

        //PESQUISAR SUBCATEGORIA
        public static void Pesquisar(List<Subcategoria> listaS, List<Categoria> listaC)
        {
            Console.Clear();
            Console.WriteLine("PESQUISAR SUBCATEGORIA\n");
            Console.Write("Digite uma subcategoria que deseja pesquisar: ");
            var pesquisa = Console.ReadLine();

            try
            {
                VerificarTermoDePesquisa(pesquisa);
            }
            catch (NomeException)
            {
                throw;
            }

            Console.WriteLine("\nRESULTADOS:\n");

            MostrarSubcategoriasCompativeis(pesquisa, listaS, listaC);

            Console.WriteLine("\nTecle Enter para retornar ao Menu Principal.");
            Console.ReadLine();
        }

        //REMOVER SUBCATEGORIA
        public static void Remover(List<Subcategoria> listaS, List<Categoria> listaC)
        {
            Console.Clear();
            Console.WriteLine("REMOVER SUBCATEGORIA\n");
            Console.WriteLine("Qual subcategoria deseja excluir?");
            int indiceS;

            try
            {
                indiceS = PesquisarSubPeloNome(listaS);
            }
            catch (NomeException)
            {
                throw;
            }

            if (indiceS == -1)
            {
                throw new NomeException("Atenção: A subcategoria não existe.");
            }

            var indiceC = PesquisarIndiceCat(listaS, listaC, indiceS);

            Console.WriteLine($"{listaS[indiceS].Nome}, que está cadastrada em {listaC[indiceC].Nome}," +
                $" será excluída.\nDeseja prosseguir?");
            var loop = "loop";
            while(loop == "loop")
            {
                Console.WriteLine("[1] Sim ou [2] Não\n ");
                var opcao = Console.ReadLine();
                if(opcao == "2")
                {
                    return;
                }
                if(opcao == "1")
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Opção inválida. Deseja excluir {listaC[indiceC].Nome}?");
                }
            }

            listaS.Remove(listaS[indiceS]);
            Console.WriteLine("\nSubcategoria removida com sucesso. Tecle Enter para retornar ao Menu Principal.");
            Console.ReadLine();
        }

        private static int PesquisarSubPeloNome(List<Subcategoria> listaS) //Pesquisa uma subcategoria cadastrada pelo nome.
        {
            Console.Write("\nInsira o nome da subcategoria: ");
            string pesquisa = Console.ReadLine();
            try
            {
                VerificarTermoDePesquisa(pesquisa);
            }
            catch (NomeException)
            {
                throw;
            }

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            pesquisa = pesquisa.ToLower();
            pesquisa = ti.ToTitleCase(pesquisa);
            foreach (Subcategoria subcategoria in listaS)
            {
                if (pesquisa == subcategoria.Nome)
                {
                    return listaS.IndexOf(subcategoria);
                }
            }
            return -1;
        }

        private static void MostrarSubcategoriasCompativeis(string pesquisa, List<Subcategoria> listaS, List<Categoria> listaC) //Método que exibe as subcategorias que retornam o termo pesquisado em seu Nome.
        {
            int resultadosEncontrados = 0;
            pesquisa = pesquisa.ToLower();
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            foreach (Subcategoria subcategoria in listaS)
            {
                if (subcategoria.Nome.Contains(pesquisa) || subcategoria.Nome.Contains(ti.ToTitleCase(pesquisa)))
                {
                    string mostrasts;
                    if (subcategoria.Status == true)
                        mostrasts = "Ativo";
                    else
                        mostrasts = "Inativo";

                    var indiceC = PesquisarIndiceCat(listaS, listaC, listaS.IndexOf(subcategoria));

                    Console.WriteLine($"Nome da subcategoria:       {subcategoria.Nome}.");
                    Console.WriteLine($"Status:                     {mostrasts}");
                    Console.WriteLine($"Data de criação:            {subcategoria.DataEHoraCriacao}");
                    if (subcategoria.DataEHoraModificacao != ValorPadrao)
                        Console.WriteLine($"Última modificação:         {subcategoria.DataEHoraModificacao}");
                    else
                        Console.WriteLine($"Data de modificação:        Não houve modificações.");
                    Console.WriteLine($"Subcategoria cadastrada em: {listaC[indiceC].Nome}\n");
                    resultadosEncontrados++;
                }
            }

            if (resultadosEncontrados == 0)
                Console.WriteLine("\nNão foram encontradas subcategorias com o termo pesquisado.\n");
            else if (resultadosEncontrados == 1)
                Console.WriteLine($"\nFoi encontrado {resultadosEncontrados} resultado.\n");
            else
                Console.WriteLine($"\nForam encontrados {resultadosEncontrados} resultados.\n");
        }

        private static int PesquisarIndiceCat(List<Subcategoria> listaS, List<Categoria> listaC, int indiceS) //Método que pesquisa o índice da categoria a partir da subcategoria.
        {
            foreach (Categoria categoria in listaC)
            {
                if (listaS[indiceS].CodigoID == categoria.ID)
                {
                    return listaC.IndexOf(categoria);
                }
            }
            return -1;
        }
    }
}
