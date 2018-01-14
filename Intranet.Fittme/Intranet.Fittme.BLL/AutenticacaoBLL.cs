using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Fittme.BLL
{
    public class AutenticacaoBLL : IAutenticacaoBLL
    {
        private IAutenticacaoDAL _autenticacaoDAL;
        public AutenticacaoBLL(IAutenticacaoDAL autenticacaoDAL)
        {
            _autenticacaoDAL = autenticacaoDAL;
        }
    }
}
