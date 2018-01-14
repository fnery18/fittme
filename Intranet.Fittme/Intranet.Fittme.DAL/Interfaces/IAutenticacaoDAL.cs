using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Fittme.MOD;

namespace Intranet.Fittme.DAL.Interfaces
{
    public interface IAutenticacaoDAL
    {
        Task<AutenticacaoMOD> Teste();
        Task<AutenticacaoMOD> RetornaUsuario(AutenticacaoMOD usuario);
    }
}
