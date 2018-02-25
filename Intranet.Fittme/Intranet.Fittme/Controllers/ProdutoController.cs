using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Intranet.Fittme.Controllers
{
    [SessionCheck]
    public class ProdutoController : Controller
    {
        private IProdutoBLL _produtoBLL;

        public ProdutoController(IProdutoBLL produtoBLL)
        {
            _produtoBLL = produtoBLL;
        }
        #region VENDAS
        public ActionResult Vender()
        {
            return View("Venda/Vender");
        }

        public ActionResult RelatorioVendas()
        {
            return View();
        }
        #endregion
        #region Produto
        [HttpGet]
        public async Task<JsonResult> BuscaProduto(string codigoProduto, int quantidadeTotal, int quantidadeEscolhida)
        {
            var produto = await _produtoBLL.BuscaProduto(codigoProduto, quantidadeTotal);

            if (produto != null)
            {
                var model = new ProdutoViewModel()
                {
                    CodigoProduto = produto.CodigoProduto,
                    Cor = produto.Cor,
                    Fornecedor = produto.Fornecedor,
                    Nome = produto.Nome,
                    NomeArquivo = produto.NomeArquivo,
                    PrecoVenda = produto.PrecoVenda,
                    Quantidade = produto.Quantidade,
                    Tamanho = produto.Tamanho,
                    QuantidadeEscolhida = quantidadeEscolhida
                };
                return Json(new { Sucesso = true, Retorno = model }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Sucesso = false, Mensagem = "Produto não encontrado ou não disponível no estoque." }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public PartialViewResult GeraDivProduto(ProdutoViewModel produto)
        {
            return PartialView("Venda/_ProdutoPartial", produto);
        }
        #endregion

    }
}