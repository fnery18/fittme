using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.DAL.Interfaces;
using Intranet.Fittme.MOD;
using System.Threading.Tasks;

namespace Intranet.Fittme.BLL
{
    public class ProdutoBLL : IProdutoBLL
    {
        private IProdutoDAL _produtoDAL;
        public ProdutoBLL(IProdutoDAL produtoDAL)
        {
            _produtoDAL = produtoDAL;
        }

        public async Task<ProdutoViewMOD> BuscaProduto(string codigoProduto, int quantidadeTotalVenda)
        {
            var produto = await _produtoDAL.BuscaProduto(codigoProduto);

            if (produto?.Quantidade >= quantidadeTotalVenda)
            {
                return produto;
            }
            return null;
        }
    }
}
