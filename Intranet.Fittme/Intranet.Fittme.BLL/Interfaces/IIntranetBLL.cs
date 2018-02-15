using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Fittme.MOD;

namespace Intranet.Fittme.BLL.Interfaces
{
    public interface IIntranetBLL
    {
        #region Fornecedor
        Task<bool> CadastraFornecedor(FornecedorMOD fornecedor);
        Task<List<FornecedorMOD>> BuscaFornecedores();
        Task<bool> AlteraFornecedor(FornecedorMOD fornecedor);
        Task<bool> ExcluiFornecedor(int codigo);
        #endregion

        #region Configuracoes
        Task<PropriedadesMOD> BuscaPropriedades();
        //TIPO
        Task<bool> CadastraTipo(TipoMOD tipo);
        Task<bool> AlteraTipo(TipoMOD tipo);
        Task<bool> ExcluiTipo(TipoMOD tipo);
        //COR
        Task<bool> AlteraCor(CorMOD cor);
        Task<bool> CadastraCor(CorMOD cor);
        Task<bool> ExcluiCor(CorMOD cor);
        #endregion

        #region Produto
        Task<bool> CadastraProduto(ProdutoMOD produto);
        Task<List<ProdutoViewMOD>> BuscaProdutos();
        Task<ProdutoViewMOD> BuscaDetalhesProduto(string codigoProduto);
        Task<bool> ExcluirProduto(string codigoProduto);
        #endregion
    }
}
