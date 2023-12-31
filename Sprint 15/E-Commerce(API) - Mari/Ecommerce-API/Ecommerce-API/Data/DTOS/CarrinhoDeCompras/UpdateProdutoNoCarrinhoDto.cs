﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Data.DTOS.CarrinhoDeCompras
{
    public class UpdateProdutoNoCarrinhoDto
    {
        [Required]
        public int CarrinhoId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int Quantidade { get; set; }
        
    }
}
