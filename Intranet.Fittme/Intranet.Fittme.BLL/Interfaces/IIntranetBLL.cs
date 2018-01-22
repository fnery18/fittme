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
        Task<bool> CadastraFornecedor(FornecedorMOD fornecedor);
        Task<List<FornecedorMOD>> BuscaFornecedores();
    }
}
