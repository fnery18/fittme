using Intranet.Fittme.MOD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intranet.Fittme.DAL.Interfaces
{
    public interface IIntranetDAL
    {
        #region FORNECEDOR
        Task<bool> CadastraFornecedor(FornecedorMOD fornecedor);
        Task<bool> AlteraFornecedor(FornecedorMOD fornecedor);
        Task<bool> ExcluiFornecedor(int codigo);
        Task<List<FornecedorMOD>> BuscaFornecedores();
        #endregion

        #region CONFIGURACOES
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
        #endregion

        #region CLIENTE
        Task<bool> CadastraCliente(ClienteMOD cliente);
        Task<bool> AlteraCliente(ClienteMOD cliente);
        Task<List<ClienteMOD>> BuscaClientes();
        Task<bool> ExcluiCliente(int codigo);
        #endregion
    }
}
