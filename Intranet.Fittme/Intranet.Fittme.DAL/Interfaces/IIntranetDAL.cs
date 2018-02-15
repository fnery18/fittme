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
        #region Fornecedor
        Task<int> CadastraFornecedor(FornecedorMOD fornecedor);
        Task<int> AlteraFornecedor(FornecedorMOD fornecedor);
        Task<int> ExcluiFornecedor(int codigo);
        Task<List<FornecedorMOD>> BuscaFornecedores();
        #endregion

        #region Produto
        Task<int> CadastraProduto(ProdutoMOD produto);
        Task<List<ProdutoViewMOD>> BuscaProdutos();
        Task<List<TipoMOD>> BuscaTamanhosDisponiveis(string nomeProduto, string fornecedor);
        Task<List<CorMOD>> BuscaCoresDisponiveis(string nomeProduto, string fornecedor);
        Task<ProdutoViewMOD> BuscaDetalhesProduto(string codigoProduto);
        #endregion

        #region Configuracoes
        Task<PropriedadesMOD> BuscaPropriedades();
        //Tipo
        Task<int> AlteraTipo(TipoMOD tipo);
        Task<int> CadastraTipo(TipoMOD tipo);
        Task<int> ExcluiTipo(int codigo);
        string BuscaTipo(int codigoTipo);
        //Cor
        Task<int> AlteraCor(CorMOD cor);
        Task<int> CadastraCor(CorMOD cor);
        Task<int> ExcluiCor(int codigo);
        string BuscaCodigoCor(int codigoCor);
        Task<bool> ExcluiProduto(string codigoProduto);
        #endregion

    }
}
