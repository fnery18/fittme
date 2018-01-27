using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intranet.Fittme.Models
{
    public class ProdutoModel
    {
        [Required, MaxLength(20)]
        public string Codigo_Produto { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public int Codigo_Tipo { get; set; }

        [Required]
        public int Codigo_Fornecedor { get; set; }

        [Required]
        public HttpPostedFileBase Imagem { get; set; }

        [Required]
        public decimal Preco { get; set; }
    }
}