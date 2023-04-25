using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Categorias;
using NomeException;

namespace SubCategorias
{
    public class SubCategoria
    {
        public bool autenticar;
        public string NomeSub { get; set; }
        public bool StatusSub { get; set; }
        public System.DateTime DataCriacaoSub { get; set; }
        public System.DateTime DataAlteracaoSub { get; set; }

        //Método para realizar as exceções
        public void VerificarNome(SubCategoria verificar)
        {
            if (string.IsNullOrEmpty(verificar.NomeSub))

            {
                throw new NomeExceptions("O nome da categoria/subcategoria não pode ser vazia!");
            }

            if (verificar.NomeSub.Length > 128)
            {
                throw new NomeExceptions("O nome da categoria/subcategoria deve conter apenas 128 caracteres.");
            }

            if (!Regex.IsMatch(verificar.NomeSub, "^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ'\\s]+$")) //é usado para validar uma cadeia de caracteres ou para garantir que uma cadeia de caracteres esteja em conformidade
            {
                throw new NomeExceptions("O nome da categoria/subcategoria deve conter somente letras do alfabeto.");
            }

            if (Regex.IsMatch(verificar.NomeSub, "^[ ]*$")) //para espaços vazios
            {
                throw new NomeExceptions("O nome da categoria/subcategoria não pode ser vazia!");
            }
        }

    }

  
    
}

