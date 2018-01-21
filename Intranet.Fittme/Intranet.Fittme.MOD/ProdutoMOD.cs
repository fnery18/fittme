using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Fittme.MOD
{
    public class ProdutoMOD
    {
        public int Codigo { get; set; } // cod linha banco
        public string Codigo_Produto { get; set; } // hash do produto 
        public string Nome { get; set; }
        public string Tipo { get; set; } // TAMANHO / NUMERACAO PMG ETC
        public string Imagem { get; set; } // URL
        public string Fornecedor { get; set; }
        public bool Estoque { get;set; }
    }
}
