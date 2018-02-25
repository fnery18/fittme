using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.MOD;
using Intranet.Fittme.MOD.Venda;
using Intranet.Fittme.Models;
using Intranet.Fittme.Models.Venda;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Intranet.Fittme.Controllers
{
    [SessionCheck]
    public class ProdutoController : Controller
    {
        private IProdutoBLL _produtoBLL;
        private IIntranetBLL _intranetBLL;

        public ProdutoController(IProdutoBLL produtoBLL, IIntranetBLL intranetBLL)
        {
            _produtoBLL = produtoBLL;
            _intranetBLL = intranetBLL;
        }

        #region VENDAS
        [HttpGet]
        public ActionResult Vender()
        {
            return View("Venda/Vender");
        }
        [HttpGet]
        public ActionResult RelatorioVendas()
        {
            return View();
        }
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

        [HttpPost]
        public async Task<JsonResult> FinalizaVenda(List<VendaModel> model)
        {
            if (ModelState.IsValid)
            {
                var produtos = model.Select(c => new VendaMOD()
                {
                    CodigoProduto = c.CodigoProduto,
                    QuantidadeEscolhida = c.QuantidadeEscolhida
                }).ToList();

                bool vendeu = await _produtoBLL.VendeProdutos(produtos);
                if (vendeu)
                    return Json(new { Sucesso = true });
                return Json(new { Sucesso = false, Mensagem = "Ops! Ocorreu um erro ao vender." });
            }
            return Json(new { Sucesso = false, Mensagem = RetornaErro(ModelState) });
        }

        #endregion

        #region PRODUTOS
        public async Task<ActionResult> Cadastrar()
        {
            PropriedadesModel model = new PropriedadesModel((await _intranetBLL.BuscaPropriedades()));
            return View("Produtos/Cadastrar", model);
        }
        public async Task<ActionResult> Listar()
        {
            var produtos = (await _produtoBLL.BuscaProdutos())
                                                .Select(c => new ProdutoViewModel(c))
                                                .ToList();
            return View("Produtos/Listar", produtos);
        }
        public async Task<ActionResult> BuscaDetalhesProduto(string codigoProduto)
        {
            var produto = new ProdutoViewModel((await _produtoBLL.BuscaDetalhesProduto(codigoProduto)));
            return PartialView("Produtos/_DetalhesPartial", produto);
        }
        [HttpPost]
        public async Task<JsonResult> CadastraProduto(ProdutoModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Imagem.ContentType.Contains("image"))
                {
                    ProdutoMOD produto = new ProdutoMOD()
                    {
                        CodigoFornecedor = model.CodigoFornecedor,
                        CodigoProdutoFornecedor = model.CodigoProdutoFornecedor,
                        CodigoProduto = model.CodigoProduto,
                        Imagem = model.Imagem,
                        Quantidade = model.Quantidade,
                        CodigoTipo = model.CodigoTipo,
                        Nome = model.Nome,
                        PrecoCusto = model.PrecoCusto,
                        PrecoNota = model.PrecoNota,
                        PrecoVenda = model.PrecoVenda,
                        CodigoCor = model.CodigoCor
                    };

                    var cadastrou = await _produtoBLL.CadastraProduto(produto);

                    if (cadastrou)
                        return Json(new { Sucesso = true });
                    else
                        return Json(new { Sucesso = false, Mensagem = "Ops, ocorreu um erro ao cadastrar" });
                }
            }

            return Json(new
            {
                Sucesso = false,
                Mensagem = "Erro, campos não preenchidos corretamente."
            });
        }

        [HttpPost]
        public async Task<JsonResult> ExcluiProduto(string codigoProduto)
        {
            bool excluio = await _produtoBLL.ExcluirProduto(codigoProduto);

            if (excluio)
                return Json(new { Sucesso = true });

            return Json(new { Sucesso = false, Mensagem = "Ocorreu um erro ao excluir o produto" });
        }

        public async Task<PartialViewResult> RetornaListaProdutos()
        {
            var produtos = (await _produtoBLL.BuscaProdutos())
                                                .Select(c => new ProdutoViewModel(c))
                                                .ToList();
            return PartialView("Produtos/_ProdutosPartial", produtos);
        }

        [HttpPost]
        public async Task<JsonResult> AlteraProduto(ProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var produto = new ProdutoViewMOD()
                {
                    CodigoProduto = model.CodigoProduto,
                    Nome = model.Nome,
                    PrecoCusto = model.PrecoCusto,
                    PrecoVenda = model.PrecoVenda,
                    PrecoNota = model.PrecoNota
                };

                bool alterou = await _produtoBLL.AlteraProduto(produto);

                if (alterou)
                    return Json(new { Sucesso = true });
                return Json(new { Sucesso = false, Mensagem = "Ops! Ocorreu um erro ao alterar o produto." });
            }

            return Json(new { Sucesso = false, Mensagem = "Erro, Campos não preenchidos corretamente." });
        }
        #endregion

        #region FUNÇÕES
        private string RetornaErro(ModelStateDictionary model)
        {
            return ModelState.Select(x => x.Value.Errors)
                          .FirstOrDefault(y => y.Count > 0)
                          .Select(z => z.ErrorMessage).FirstOrDefault();
        }
        #endregion

    }
}