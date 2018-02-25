using Intranet.Fittme.MOD;
using System.Threading.Tasks;

namespace Intranet.Fittme.DAL.Interfaces
{
    public interface IProdutoDAL
    {
        Task<ProdutoViewMOD> BuscaProduto(string codigoProduto);
    }
}
