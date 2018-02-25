using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.DAL.Interfaces;
using Intranet.Fittme.MOD;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Intranet.Fittme.BLL
{
    public class IntranetBLL : IIntranetBLL
    {
        private IIntranetDAL _intranetDAL;
        public IntranetBLL(IIntranetDAL intranetDAL)
        {
            _intranetDAL = intranetDAL;
        }

        #region FORNECEDOR
        public async Task<bool> CadastraFornecedor(FornecedorMOD fornecedor)
        {
            return await _intranetDAL.CadastraFornecedor(fornecedor);
        }
        public async Task<bool> AlteraFornecedor(FornecedorMOD fornecedor)
        {
            return await _intranetDAL.AlteraFornecedor(fornecedor);
        }
        public async Task<List<FornecedorMOD>> BuscaFornecedores()
        {
            return await _intranetDAL.BuscaFornecedores();
        }
        public async Task<bool> ExcluiFornecedor(int codigo)
        {
            return await _intranetDAL.ExcluiFornecedor(codigo);
        }
        #endregion

        #region CONFIGURACOES
        public async Task<PropriedadesMOD> BuscaPropriedades()
        {
            return await _intranetDAL.BuscaPropriedades();
        }
        //COR
        public async Task<bool> CadastraCor(CorMOD cor)
        {
            return await _intranetDAL.CadastraCor(cor) > 0;
        }
        public async Task<bool> AlteraCor(CorMOD cor)
        {
            return await _intranetDAL.AlteraCor(cor) > 0;
        }
        public async Task<bool> ExcluiCor(CorMOD cor)
        {
            return await _intranetDAL.ExcluiCor(cor.Codigo) > 0;
        }

        //TIPO
        public async Task<bool> AlteraTipo(TipoMOD tipo)
        {
            return await _intranetDAL.AlteraTipo(tipo) > 0;
        }
        public async Task<bool> CadastraTipo(TipoMOD tipo)
        {
            return await _intranetDAL.CadastraTipo(tipo) > 0;
        }
        public async Task<bool> ExcluiTipo(TipoMOD tipo)
        {
            return await _intranetDAL.ExcluiTipo(tipo.Codigo) > 0;
        }
        #endregion

        #region CLIENTE
        public async Task<bool> CadastraCliente(ClienteMOD cliente)
        {
            return await _intranetDAL.CadastraCliente(cliente);
        }

        public async Task<bool> ExcluiCliente(int codigo)
        {
            return await _intranetDAL.ExcluiCliente(codigo);
        }

        public async Task<List<ClienteMOD>> BuscaClientes()
        {
            return await _intranetDAL.BuscaClientes();
        }

        public async Task<bool> AlteraCliente(ClienteMOD cliente)
        {
            return await _intranetDAL.AlteraCliente(cliente);
        }
        #endregion
    }
}
