using Intranet.Fittme.MOD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intranet.Fittme.BLL.Interfaces
{
    public interface IIntranetBLL
    {
        #region FORNECEDOR
        Task<bool> CadastraFornecedor(FornecedorMOD fornecedor);
        Task<List<FornecedorMOD>> BuscaFornecedores();
        Task<bool> AlteraFornecedor(FornecedorMOD fornecedor);
        Task<bool> ExcluiFornecedor(int codigo);
        #endregion

        #region CONFIGURACOES
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
    }
}
