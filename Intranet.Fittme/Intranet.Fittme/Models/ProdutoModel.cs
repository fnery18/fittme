using Intranet.Fittme.MOD;
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
        public string CodigoProdutoFornecedor { get; set; }

        [Required]
        public string CodigoProduto { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public int CodigoTipo { get; set; }

        [Required]
        public int CodigoFornecedor { get; set; }

        [Required]
        public HttpPostedFileBase Imagem { get; set; }
        public string NomeArquivo {get;set;}

        [Required]
        public decimal PrecoCusto { get; set; }

        [Required]
        public decimal PrecoNota { get; set; }

        [Required]
        public decimal PrecoVenda { get; set; }

        [Required]
        public int CodigoCor { get; set; }

        public ProdutoModel() { }
        public ProdutoModel(ProdutoMOD produto)
        {
            CodigoProdutoFornecedor = produto.CodigoProdutoFornecedor;
            CodigoProduto = produto.CodigoProduto;
            CodigoCor = produto.CodigoCor;
            Nome = produto.Nome;
            CodigoTipo = produto.CodigoTipo;
            Imagem = produto.Imagem;
            NomeArquivo = produto.NomeArquivo;
            CodigoFornecedor = produto.CodigoFornecedor;
            PrecoCusto = produto.PrecoCusto;
            PrecoNota = produto.PrecoNota;
            PrecoVenda = produto.PrecoVenda;
            Quantidade = produto.Quantidade;
        }
    }
}