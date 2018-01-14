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
    public class AutenticacaoBLL : IAutenticacaoBLL
    {
        private IAutenticacaoDAL _autenticacaoDAL;
        public AutenticacaoBLL(IAutenticacaoDAL autenticacaoDAL)
        {
            _autenticacaoDAL = autenticacaoDAL;
        }

        public async Task<AutenticacaoMOD> Teste()
        {
            return await _autenticacaoDAL.Teste();
        }

        public async Task<bool> ValidaUsuario(AutenticacaoMOD usuario)
        {
            return await _autenticacaoDAL.RetornaUsuario(usuario) != null;
        }
    }
}
