using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Fittme.DAL.Interfaces
{
    public interface IIntranetDAL
    {
        Task<int> CadastraFornecedor(FornecedorMOD fornecedor);
        Task<List<FornecedorMOD>> BuscaFornecedores();
        Task<PropriedadesMOD> BuscaPropriedades();
        Task<int> CadastraProduto(ProdutoMOD produto, string caminho);
        Task<int> AlteraFornecedor(FornecedorMOD fornecedor);
        Task<int> ExcluiFornecedor(int codigo);
    }
}
