using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intranet.Fittme.BLL.Interfaces
{
    public interface IAutenticacaoBLL
    {
        Task<AutenticacaoMOD> Teste();
    }
}
