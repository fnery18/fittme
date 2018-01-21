using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Fittme.MOD;

namespace Intranet.Fittme.BLL
{
    public class IntranetBLL : IIntranetBLL
    {
        private IIntranetDAL _intranetDAL;
        public IntranetBLL(IIntranetDAL intranetDAL)
        {
            _intranetDAL = intranetDAL;
        }

        public async Task<bool> CadastraFornecedor(FornecedorMOD fornecedor)
        {
            return await _intranetDAL.CadastraFornecedor(fornecedor) > 0;
        }
    }
}
