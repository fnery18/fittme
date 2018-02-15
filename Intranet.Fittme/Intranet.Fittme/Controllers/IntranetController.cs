using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.MOD;
using Intranet.Fittme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Intranet.Fittme.Controllers
{
    [SessionCheck]
    public class IntranetController : Controller
    {

        private IIntranetBLL _intranetBLL;
        public IntranetController(IIntranetBLL intranetBLL)
        {
            _intranetBLL = intranetBLL;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Produtos
        public async Task<ActionResult> Produto()
        {
            PropriedadesModel model = new PropriedadesModel((await _intranetBLL.BuscaPropriedades()));
            return View("Produtos/Cadastrar", model);
        }
        public async Task<ActionResult> ListarProdutos()
        {
            var produtos = (await _intranetBLL.BuscaProdutos())
                                                .Select(c => new ProdutoViewModel(c))
                                                .ToList();
            return View("Produtos/Listar", produtos);
        }
        public async Task<ActionResult> BuscaDetalhesProduto(string codigoProduto)
        {
            var produto = new ProdutoViewModel((await _intranetBLL.BuscaDetalhesProduto(codigoProduto)));
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

                    var cadastrou = await _intranetBLL.CadastraProduto(produto);

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
            bool excluio = await _intranetBLL.ExcluirProduto(codigoProduto);

            if (excluio)
                return Json(new { Sucesso = true });

            return Json(new { Sucesso = false, Mensagem = "Ocorreu um erro ao excluir o produto" });
        }

        public async Task<PartialViewResult> RetornaListaProdutos()
        {
            var produtos = (await _intranetBLL.BuscaProdutos())
                                                .Select(c => new ProdutoViewModel(c))
                                                .ToList();
            return PartialView("Produtos/_ProdutosPartial", produtos);
        }
        #endregion

        #region Clientes
        public ActionResult Cliente()
        {
            return View("Clientes/Cadastrar");
        }
        public ActionResult ListarClientes()
        {
            return View("Clientes/Listar");
        }
        #endregion

        #region Vendas
        public ActionResult Venda()
        {
            return View("Vendas/Cadastrar");
        }
        public ActionResult ListarVendas()
        {
            return View("Vendas/Listar");
        }
        #endregion

        #region Fornecedor
        public ActionResult Fornecedor()
        {
            return View("Fornecedores/Cadastrar");
        }

        [HttpPost]
        public async Task<ActionResult> CadastraFornecedor(FornecedorModel model)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = new FornecedorMOD
                {
                    Nome = model.Nome,
                    Celular = model.Celular,
                    Email = model.Email
                };

                bool cadastrou = await _intranetBLL.CadastraFornecedor(fornecedor);

                if (cadastrou)
                    return Json(new { Sucesso = true });

                return Json(new { Sucesso = false, Mensagem = "Ops, ocorreu um erro ao cadastrar esse fornecedor" });
            }
            return Json(new { Sucesso = false, Mensagem = "Ops, campos não preenchidos corretamente" });
        }
        public ActionResult Fornecedores()
        {
            return View("Fornecedores/Listar");
        }

        [HttpGet]
        public async Task<ActionResult> TabelaFornecedores()
        {
            var model = (await _intranetBLL.BuscaFornecedores())
                                              .Select(c => new FornecedorModel(c))
                                                  .ToList();

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("Fornecedores/_TabelaFornecedoresPartial", model)
                : View("Fornecedores/Listar");
        }
        [HttpPost]
        public async Task<JsonResult> AlteraFornecedor(FornecedorModel model)
        {
            if (ModelState.IsValid)
            {
                var fornecedor = new FornecedorMOD
                {
                    Codigo = model.Codigo,
                    Nome = model.Nome,
                    Celular = model.Celular,
                    Email = model.Email
                };
                bool alterou = await _intranetBLL.AlteraFornecedor(fornecedor);
                if (alterou)
                    return Json(new { Sucesso = true });

                return Json(new { Sucesso = false, Mensagem = "Ops, ocorreu um erro ao alterar esse fornecedor" });
            }
            return Json(new { Sucesso = false, Mensagem = "Ops, campos não preenchidos corretamente" });
        }

        [HttpPost]
        public async Task<JsonResult> ExcluiFornecedor(int codigo)
        {
            return Json(new { Sucesso = await _intranetBLL.ExcluiFornecedor(codigo) });
        }
        #endregion

        #region Usuarios
        public ActionResult Usuario()
        {
            return View("Usuario/Cadastrar");
        }
        public ActionResult ListarUsuarios()
        {
            return View("Usuario/Listar");
        }
        #endregion

        #region Configuracoes

        public ActionResult Configuracoes()
        {
            return View("Config/Configuracoes");
        }

        [HttpGet]
        public async Task<ActionResult> TabelaCores()
        {
            PropriedadesModel model = new PropriedadesModel((await _intranetBLL.BuscaPropriedades()));

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("Config/_TabelaCoresPartial", model.Cores)
                : View("Config/Configuracoes");
        }

        [HttpGet]
        public async Task<ActionResult> TabelaTipos()
        {
            PropriedadesModel model = new PropriedadesModel((await _intranetBLL.BuscaPropriedades()));

            return Request.IsAjaxRequest()
                ? (ActionResult)PartialView("Config/_TabelaTiposPartial", model.Tipos)
                : View("Config/Configuracoes");
        }

        [HttpPost]
        public async Task<ActionResult> CadastraTipo(TipoModel model)
        {
            if (ModelState.IsValid)
            {
                TipoMOD tipo = new TipoMOD { Nome = model.Nome };
                bool cadastrou = await _intranetBLL.CadastraTipo(tipo);

                if (cadastrou)
                    return Json(new { Sucesso = true });
                return Json(new { Sucesso = false, Mensagem = "Ops, Ocorreu um erro ao cadastrar o tipo." });
            }

            return Json(new { Sucesso = false, Mensagem = "Ops, Campos não preenchidos corretamente." });
        }
        [HttpPost]
        public async Task<ActionResult> AlteraTipo(TipoModel model)
        {
            if (ModelState.IsValid)
            {
                TipoMOD tipo = new TipoMOD { Nome = model.Nome, Codigo = model.Codigo };
                bool alterou = await _intranetBLL.AlteraTipo(tipo);

                if (alterou)
                    return Json(new { Sucesso = true });
                return Json(new { Sucesso = false, Mensagem = "Ops, Ocorreu um erro ao editar o tipo." });
            }

            return Json(new { Sucesso = false, Mensagem = "Ops, Campos não preenchidos corretamente." });
        }
        [HttpPost]
        public async Task<ActionResult> ExcluiTipo(TipoModel model)
        {
            if (ModelState.IsValid)
            {
                TipoMOD tipo = new TipoMOD { Codigo = model.Codigo };
                bool excluio = await _intranetBLL.ExcluiTipo(tipo);

                if (excluio)
                    return Json(new { Sucesso = true });
                return Json(new { Sucesso = false, Mensagem = "Ops, Ocorreu um erro ao excluir o tipo." });
            }

            return Json(new { Sucesso = false, Mensagem = "Ops, Campos não preenchidos corretamente." });
        }

        [HttpPost]
        public async Task<ActionResult> CadastraCor(CorModel model)
        {
            if (ModelState.IsValid)
            {
                CorMOD cor = new CorMOD
                {
                    Nome = model.Nome,
                    Codigo = model.Codigo,
                    CodigoCor = model.CodigoCor,
                    Cor = model.Cor
                };
                bool excluio = await _intranetBLL.CadastraCor(cor);

                if (excluio)
                    return Json(new { Sucesso = true });
                return Json(new { Sucesso = false, Mensagem = "Ops, Ocorreu um erro ao cadastrar a cor." });
            }

            return Json(new { Sucesso = false, Mensagem = "Ops, Campos não preenchidos corretamente." });
        }
        [HttpPost]
        public async Task<ActionResult> AlteraCor(CorModel model)
        {
            if (ModelState.IsValid)
            {
                CorMOD cor = new CorMOD
                {
                    Nome = model.Nome,
                    Codigo = model.Codigo,
                    CodigoCor = model.CodigoCor,
                    Cor = model.Cor
                };
                bool alterou = await _intranetBLL.AlteraCor(cor);

                if (alterou)
                    return Json(new { Sucesso = true });
                return Json(new { Sucesso = false, Mensagem = "Ops, Ocorreu um erro ao editar a cor." });
            }

            return Json(new { Sucesso = false, Mensagem = "Ops, Campos não preenchidos corretamente." });
        }
        [HttpPost]
        public async Task<ActionResult> ExcluiCor(CorModel model)
        {
            if (ModelState.IsValid)
            {
                CorMOD cor = new CorMOD
                {
                    Nome = model.Nome,
                    Codigo = model.Codigo,
                    CodigoCor = model.CodigoCor,
                    Cor = model.Cor
                };
                bool excluio = await _intranetBLL.ExcluiCor(cor);

                if (excluio)
                    return Json(new { Sucesso = true });
                return Json(new { Sucesso = false, Mensagem = "Ops, Ocorreu um erro ao excluir a cor." });
            }

            return Json(new { Sucesso = false, Mensagem = "Ops, Campos não preenchidos corretamente." });
        }
        #endregion

    }
}