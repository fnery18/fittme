using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Fittme.Models
{
    public class ProdutoViewModel
    {
        public string Fornecedor { get; set; }

        public string CodigoProduto { get; set; }

        public string Nome { get; set; }

        public int Quantidade { get; set; }
        public bool EmEstoque { get; set; }

        public int Tamanho { get; set; }
        public List<TipoModel> TamanhosDisponiveis { get; set; }

        public string NomeArquivo { get; set; }
        public decimal PrecoCusto { get; set; }

        public decimal PrecoNota { get; set; }

        public decimal PrecoVenda { get; set; }

        public string Cor { get; set; }
        public List<CorModel> CoresDisponiveis { get; set; }
    }
}