using Intranet.Fittme.MOD;
using System.Threading.Tasks;

namespace Intranet.Fittme.BLL.Interfaces
{
    public interface IProdutoBLL
    {
        Task<ProdutoViewMOD> BuscaProduto(string codigoProduto, int quantidadeTotalVenda);
    }
}
