using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Intranet.Fittme.MOD
{
    public class ProdutoMOD
    {
        public int Codigo { get; set; } // codigo no banco
        public string CodigoProdutoFornecedor { get; set; } // codigo do produto no fornecedor
        public string CodigoProduto { get; set; } //codigo fittme
        public int CodigoCor { get; set; }
        public string Nome { get; set; }
        public int CodigoTipo { get; set; }
        public HttpPostedFileBase Imagem { get; set; }
        public string NomeArquivo { get; set; }
        public int CodigoFornecedor { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoNota { get; set; }
        public decimal PrecoVenda { get; set; }
        public int Quantidade { get; set; }
    }
}
