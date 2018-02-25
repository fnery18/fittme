using Intranet.Fittme.MOD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intranet.Fittme.DAL.Interfaces
{
    public interface IProdutoDAL
    {
        #region PRODUTO
        Task<List<ProdutoViewMOD>> BuscaProdutos();
        Task<List<TipoMOD>> BuscaTamanhosDisponiveis(string nomeProduto, string fornecedor);
        Task<List<CorMOD>> BuscaCoresDisponiveis(string nomeProduto, string fornecedor);
        Task<ProdutoViewMOD> BuscaDetalhesProduto(string codigoProduto);
        Task<bool> ExcluiProduto(string codigoProduto);
        Task<bool> AlteraProduto(ProdutoViewMOD produto);
        Task<int> CadastraProduto(ProdutoMOD produto);
        #endregion

        #region VENDA
        Task<ProdutoViewMOD> BuscaProduto(string codigoProduto);
        #endregion
    }
}
