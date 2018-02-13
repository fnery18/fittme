using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Fittme.MOD
{
    public class ProdutoViewMOD
    {
        public string Fornecedor { get; set; }

        public string CodigoProduto { get; set; }
        public string CodigoProdutoFornecedor { get; set; }

        public string Nome { get; set; }

        public int Quantidade { get; set; }
        public bool EmEstoque { get; set; }

        public string Tamanho { get; set; }
        public List<TipoMOD> TamanhosDisponiveis { get; set; } // na hora de exibir o produto sozinho

        public string NomeArquivo { get; set; }
        public decimal PrecoCusto { get; set; }

        public decimal PrecoNota { get; set; }

        public decimal PrecoVenda { get; set; }

        public string Cor { get; set; }
        public string CorHexadecimal { get; set; }
        public List<CorMOD> CoresDisponiveis { get; set; }
    }
}
