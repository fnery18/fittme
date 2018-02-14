using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Fittme.Models
{
    public class ProdutoViewModel
    {
        public string Fornecedor { get; set; } //ok

        public string CodigoProduto { get; set; }
        public string CodigoProdutoFornecedor { get; set; }

        public string Nome { get; set; } //ok

        public int Quantidade { get; set; }
        public bool EmEstoque { get; set; }

        public string Tamanho { get; set; }
        public List<TipoModel> TamanhosDisponiveis { get; set; } // na hora de exibir o produto sozinho

        public string NomeArquivo { get; set; }//ok
        public decimal PrecoCusto { get; set; } //ok

        public decimal PrecoNota { get; set; } //ok

        public decimal PrecoVenda { get; set; } //ok

        public string Cor { get; set; }
        public string CorHexadecimal { get; set; }
        public List<CorModel> CoresDisponiveis { get; set; }

        public ProdutoViewModel(){}

        public ProdutoViewModel(ProdutoViewMOD produto)
        {
            Fornecedor = produto.Fornecedor;
            CodigoProduto = produto.CodigoProduto;
            CodigoProdutoFornecedor = produto.CodigoProdutoFornecedor;
            Nome = produto.Nome;
            Quantidade = produto.Quantidade;
            EmEstoque = produto.EmEstoque;
            Tamanho = produto.Tamanho;
            TamanhosDisponiveis = produto.TamanhosDisponiveis != null ? produto.TamanhosDisponiveis.Select(c => new TipoModel(c)).ToList() : null;
            NomeArquivo = produto.NomeArquivo;
            PrecoCusto = produto.PrecoCusto;
            PrecoNota = produto.PrecoNota;
            PrecoVenda = produto.PrecoVenda;
            Cor = produto.Cor;
            CorHexadecimal = produto.CorHexadecimal;
            CoresDisponiveis = produto.CoresDisponiveis != null ? produto.CoresDisponiveis.Select(c => new CorModel(c)).ToList() : null;
        }
    }
}