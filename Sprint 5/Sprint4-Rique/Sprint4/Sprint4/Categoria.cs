using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sprint4
{
    public class Categoria
    {
        public string Nome { get; set; }
        public bool Status { get; set; }
        public DateTime DataEHoraCriacao { get; set; }
        public DateTime DataEHoraModificacao { get; set; }
        public Guid ID { get; }
        static public DateTime ValorPadrao { get; }
        readonly static int maxCaracteres = 128;
        readonly static int minCaracteres = 3;

        public Categoria(string nome, bool status, DateTime criacao, DateTime modificacao)
        {
            Nome = nome;
            Status = status;
            DataEHoraCriacao = criacao;
            DataEHoraModificacao = modificacao;
            ID = Guid.NewGuid();
        }
        public Categoria()
        {

        }

        //CADASTRAR CATEGORIA
        public static void Cadastrar(List<Categoria> listaC, List<Subcategoria> listaS)
        {
            Console.Clear();
            Console.WriteLine("CRIAR CATEGORIA!\n\n");
            Console.Write("Defina um nome para a categoria: ");
            string nome = Console.ReadLine();

            try
            {
                nome = VerificarNome(nome, listaC.Count, listaC, listaS);
            }
            catch (NomeException)
            {
                throw;
            }

            listaC.Add(new Categoria(nome, true, DateTime.Now, ValorPadrao));

            Console.WriteLine("\nCategoria criada com sucesso!\n");
            Console.WriteLine($"Nome da categoria: {listaC[listaC.Count - 1].Nome}.");

            string mostrasts;
            if (listaC[listaC.Count - 1].Status == true)
                mostrasts = "Ativo";
            else
                mostrasts = "Inativo";

            Console.WriteLine($"Status da categoria: {mostrasts}.");
            Console.WriteLine($"Data da criação: {listaC[listaC.Count - 1].DataEHoraCriacao}");
            Console.WriteLine("Data da modificação: Não houve modificação.\n");

            Console.WriteLine("Tecle Enter para retornar ao Menu Principal.");
            Console.ReadLine();
        }

        //EDITAR CATEGORIA
        public static void Editar(List<Categoria> listaC, List<Subcategoria> listaS)
        {
            Console.Clear();
            Console.WriteLine("EDIÇÃO DE CATEGORIA");
            Console.WriteLine("\nPesquise a categoria que deseja editar.");
            int indiceC;
            try
            {
                indiceC = PesquisarCatPeloNome(listaC);
            }
            catch (NomeException)
            {
                throw;
            }

            if (indiceC == -1)
            {
                throw new NomeException("Atenção: A categoria não existe.");
            }

            Console.WriteLine($"\nVamos editar a categoria {listaC[indiceC].Nome}.");
            Console.Write("Deseja editar o Nome e Status [1], apenas o Nome [2] ou apenas o Status [3]? ");
            var opcao = Console.ReadLine();
            var novoNome = listaC[indiceC].Nome;
            var novoStatus = listaC[indiceC].Status;

            //Edita Nome e Status da Categoria
            if (opcao == "1")
            {
                Console.Write("\nDigite um novo nome para a categoria: ");
                novoNome = Console.ReadLine();
                try
                {
                    novoNome = VerificarNome(novoNome, indiceC, listaC, listaS);
                }
                catch (NomeException)
                {
                    throw;
                }
                novoStatus = EditarStatus(novoStatus);
            }

            //Edita apenas o Nome da Categoria
            else if (opcao == "2")
            {
                Console.Write("\nDigite um novo nome para a categoria: ");
                novoNome = Console.ReadLine();
                try
                {
                    novoNome = VerificarNome(novoNome, indiceC, listaC, listaS);
                }
                catch (NomeException)
                {
                    throw;
                }
            }

            //Edita apenas o Status da Categoria
            else if (opcao == "3")
            {
                novoStatus = EditarStatus(novoStatus);
            }

            // Quando uma opção inválida é digitada, retorna ao menu principal.
            else
            {
                Console.WriteLine("Opção inválida. Tecle Enter para retornar ao menu principal.");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            DateTime criacao = listaC[indiceC].DataEHoraCriacao;

            listaC.Remove(listaC[indiceC]);
            listaC.Insert(indiceC, new Categoria(novoNome, novoStatus, criacao, DateTime.Now));

            string mostrasts;
            if (novoStatus == true)
                mostrasts = "Ativa.";
            else
                mostrasts = "Inativa.";

            Console.WriteLine("\nCategoria editada com sucesso.");
            Console.WriteLine("\nNovo nome da categoria: " + listaC[indiceC].Nome);
            Console.WriteLine("Novo status da categoria: " + mostrasts);
            Console.WriteLine("Data da criação: " + listaC[indiceC].DataEHoraCriacao);
            Console.WriteLine("Data da modificação: " + listaC[indiceC].DataEHoraModificacao);

            Console.WriteLine("\nTecle Enter para retornar ao Menu Principal.");
            Console.ReadLine();
        }

        //PESQUISAR CATEGORIA
        public static void Pesquisar(List<Categoria> listaC, List<Subcategoria> listaS)
        {
            Console.Clear();
            Console.WriteLine("PESQUISAR CATEGORIA\n");
            Console.Write("Digite uma categoria que deseja pesquisar: ");
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

            MostrarCategoriasCompativeis(pesquisa, listaC, listaS);

            Console.WriteLine("\nTecle Enter para retornar ao Menu Principal.");
            Console.ReadLine();
        }

        //REMOVER CATEGORIA
        public static void Remover(List<Categoria> listaC, List<Subcategoria> listaS)
        {
            Console.Clear();
            Console.WriteLine("REMOVER CATEGORIA\n");
            Console.Write("Qual categoria deseja excluir?");
            var indiceC = PesquisarCatPeloNome(listaC);

            if (indiceC == -1)
            {
                throw new NomeException("Atenção: A categoria não existe.");
            }

            string[] arrayDeSub = new string[listaS.Count];

            int j = 0;
            for (int g = 0; g < listaS.Count; g++)
            {
                if (listaS[g].CodigoID == listaC[indiceC].ID)
                {
                    if (j == 0)
                    {
                        arrayDeSub[j] = listaS[g].Nome;
                        j++;
                    }
                    else
                    {
                        arrayDeSub[j] += listaS[g].Nome.ToLower();
                        j++;
                    }
                }
            }

            if (j != 0)
            {
                if (j == 1)
                {
                    Console.Write($"\nEm {listaC[indiceC].Nome} está cadastrada a seguinte subcategoria: " +
                        $"{arrayDeSub[0]}.\nSe continuar, ela também será excluída.\n");
                }
                else
                {
                    Console.Write($"\nEm {listaC[indiceC].Nome} estão cadastradas as seguintes subcategorias: ");
                    for (int h = 0; h < j; h++)
                    {
                        Console.Write(arrayDeSub[h]);
                        if (h < j - 1)
                            Console.Write(", ");
                    }
                    Console.WriteLine(".\n");
                    Console.WriteLine("Se continuar, todas essas subcategorias serão excluídas.\n");
                }
            }

            string opcao = "menu";
            while (opcao == "menu")
            {
                Console.Write($"Deseja excluir {listaC[indiceC].Nome}? [1] Sim ou [2] Não    ");
                opcao = Console.ReadLine();

                if (opcao == "2")
                {
                    return;
                }
                else if (opcao == "1")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.\n");
                    opcao = "menu";
                }
            }

            if (j != 0)
            {
                for (int i = 0; i < listaS.Count; i++)
                {
                    if (listaS[i].CodigoID == listaC[indiceC].ID)
                    {
                        listaS.Remove(listaS[i]);
                    }
                }
            }

            listaC.Remove(listaC[indiceC]);
            Console.WriteLine("\nCategoria removida com sucesso. Tecle enter para Retornar ao Menu Principal.");
            Console.ReadLine();
        }

        protected static int PesquisarCatPeloNome(List<Categoria> listaC) //Pesquisa uma categoria cadastrada pelo nome.
        {
            Console.Write("\nInsira o nome da categoria: ");
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
            foreach (Categoria categoria in listaC)
            {
                if (pesquisa == categoria.Nome)
                {
                    return listaC.IndexOf(categoria);
                }
            }
            return -1;
        }

        protected static void MostrarCategoriasCompativeis(string pesquisa, List<Categoria> listaC, List<Subcategoria> listaS) //Método que exibe as categorias que retornam o termo pesquisado em seu Nome.
        {
            int resultadosEncontrados = 0;
            pesquisa = pesquisa.ToLower();
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            foreach (Categoria categoria in listaC)
            {
                if (categoria.Nome.Contains(pesquisa) || categoria.Nome.Contains(ti.ToTitleCase(pesquisa)))
                {
                    string mostrasts;
                    if (categoria.Status == true)
                        mostrasts = "Ativo";
                    else
                        mostrasts = "Inativo";

                    Console.WriteLine($"Nome da categoria:          {categoria.Nome}.");
                    Console.WriteLine($"Status:                     {mostrasts}");
                    Console.WriteLine($"Data de criação:            {categoria.DataEHoraCriacao}");
                    if (categoria.DataEHoraModificacao != ValorPadrao)
                        Console.WriteLine($"Última modificação:         {categoria.DataEHoraModificacao}");
                    else
                        Console.WriteLine($"Data de modificação:        Não houve modificações.");
                    if (listaS.Count > 0)
                    {
                        string[] arrayDeSub = new string[listaS.Count];
                        int j = 0;
                        for (int g = 0; g < listaS.Count; g++)
                        {
                            if (listaS[g].CodigoID == categoria.ID)
                                if (j == 0)
                                {
                                    arrayDeSub[j] = listaS[g].Nome;
                                    j++;
                                }
                                else
                                {
                                    arrayDeSub[j] += listaS[g].Nome.ToLower();
                                    j++;
                                }
                        }
                        if (j != 0)
                        {
                            Console.Write("Subcategorias cadastradas:  ");
                            for (int h = 0; h < j; h++)
                            {
                                Console.Write(arrayDeSub[h]);
                                if (h < j - 1)
                                    Console.Write(", ");
                            }
                            Console.WriteLine(".\n");
                        }
                    }
                    else
                        Console.WriteLine("\n");
                    resultadosEncontrados++;
                }

            }

            if (resultadosEncontrados == 0)
                Console.WriteLine("\nNão foram encontradas categorias com o termo pesquisado.\n");
            else if (resultadosEncontrados == 1)
                Console.WriteLine($"\nFoi encontrado {resultadosEncontrados} resultado.\n");
            else
                Console.WriteLine($"\nForam encontrados {resultadosEncontrados} resultados.\n");
        }

        protected static string VerificarNome(string nome, int indice, List<Categoria> listaC, List<Subcategoria> listaS) //Método que verifica se o Nome está dentro dos critérios de aceite, e retorna o valor de Nome com a primeira letra em maiúsculo.
        {
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            nome = nome.ToLower();
            nome = ti.ToTitleCase(nome);

            if (listaS.Count > 0)
            {
                foreach (Subcategoria subcategoria in listaS)
                {
                    for (int i = 0; i < listaS.Count; i++)
                    {
                        if (nome == listaS[i].Nome)
                        {
                            throw new NomeException("Atenção: Já existe uma subcategoria com esse nome.");
                        }
                    }
                }
            }

            foreach (Categoria categoria in listaC)
            {
                for (int i = 0; i < listaC.Count; i++)
                {
                    if (nome == listaC[i].Nome)
                    {
                        throw new NomeException("Atenção: Já existe uma categoria com esse nome.");
                    }
                }
            }

            if (nome == "")
            {
                throw new NomeException("Atenção: O nome não pode ficar vazio.");
            }
            if (nome.Length < minCaracteres)
            {
                throw new NomeException($"Atenção: O nome não pode conter menos de {minCaracteres} caracteres.");
            }
            if (nome.Length > maxCaracteres)
            {
                throw new NomeException($"Atenção: O nome não pode contar mais de {maxCaracteres} caracteres.");
            }
            if (!nome.Replace(" ", "").All(char.IsLetter))
            {
                throw new NomeException("Atenção: O nome só pode conter letras.");
            }
            return nome;
        }

        protected static bool EditarStatus(bool statusAtual) //Método que edita o Status da Categoria.
        {
            string mostrasts;
            if (statusAtual == true)
                mostrasts = "ativo";
            else
                mostrasts = "inativo";
            Console.Write($"\nO status atual é {mostrasts}. Deseja modificar?\n [1] Sim ou [2] Não?\n  ");

            bool verificacaoStatus = true;
            while (verificacaoStatus == true)
            {
                var ler = Console.ReadLine();
                if (ler == "1")
                {
                    return !statusAtual;
                }
                if (ler == "2")
                {
                    return statusAtual;
                }
                Console.Write("\nOpção inválida. Tente novamente: [1] Modificar status ou [2] Manter o status   ");
            }
            return statusAtual;
        }

        protected static void VerificarTermoDePesquisa(string pesquisa) //Verifica os critérios de aceite para a Pesquisa de categoria||subcategoria.
        {
            if (pesquisa == "")
            {
                throw new NomeException($"Atenção: A pesquisa não pode ficar vazia. Digite mais de {minCaracteres} caracteres " +
                    $"para pesquisar.");
            }
            if (pesquisa.Length < minCaracteres)
            {
                throw new NomeException($"Atenção: Digite pelo menos {minCaracteres} caracteres para pesquisa.");
            }
            if (pesquisa.Length > maxCaracteres)
            {
                throw new NomeException($"Atenção: A pesquisa não pode conter mais de {maxCaracteres} caracteres.");
            }
            if (!pesquisa.Replace(" ", "").All(char.IsLetter))
            {
                throw new NomeException("Atenção: A pesquisa só pode conter letras.");
            }
        }
    }
}
