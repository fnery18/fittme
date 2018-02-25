using Intranet.Fittme.MOD;
using System.Collections.Generic;
using System.Threading.Tasks;
using Intranet.Fittme.MOD.Venda;

namespace Intranet.Fittme.BLL.Interfaces
{
    public interface IProdutoBLL
    {
        #region VENDA
        Task<ProdutoViewMOD> BuscaProduto(string codigoProduto, int quantidadeTotalVenda);
        #endregion

        #region PRODUTO
        Task<bool> CadastraProduto(ProdutoMOD produto);
        Task<List<ProdutoViewMOD>> BuscaProdutos();
        Task<ProdutoViewMOD> BuscaDetalhesProduto(string codigoProduto);
        Task<bool> ExcluirProduto(string codigoProduto);
        Task<bool> AlteraProduto(ProdutoViewMOD produto);
        Task<bool> VendeProdutos(List<VendaMOD> produtos);
        #endregion
    }
}
