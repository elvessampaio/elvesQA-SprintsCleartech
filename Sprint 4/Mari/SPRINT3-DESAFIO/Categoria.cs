using SPRINT3_DESAFIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NomeException;
using System.Text.RegularExpressions;
using SubCategorias;

namespace Categorias
{
    public class Categoria
    {
        public string Nome { get; set; }
        public bool Status { get; set; }
        public System.DateTime DataCriacao { get; set; }
        public System.DateTime DataAlteracao { get; set; }

        public bool autenticar;

        //Método para realizar as exceções
        public void VerificarNome(Categoria verificar)
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

 
