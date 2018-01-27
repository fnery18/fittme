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
    public class IntranetController : Controller
    {

        private IIntranetBLL _intranetBLL;
        public IntranetController(IIntranetBLL intranetBLL)
        {
            _intranetBLL = intranetBLL;
        }
        //[SessionCheck]
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
            return View("Produtos/Listar");
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
                        Codigo_Fornecedor = model.Codigo_Fornecedor,
                        Codigo_Produto = model.Codigo_Produto,
                        Imagem = model.Imagem,
                        Quantidade = model.Quantidade,
                        Codigo_Tipo = model.Codigo_Tipo,
                        Nome = model.Nome,
                        Preco = model.Preco
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
                {
                    return Json(new
                    {
                        Sucesso = true
                    });
                }

                return Json(new
                {
                    Sucesso = false,
                    Mensagem = "Ops, ocorreu um erro ao cadastrar esse fornecedor"
                });
            }

            return Json(new
            {
                Sucesso = false,
                Mensagem = "Ops, campos não preenchidos corretamente"
            });
        }
        public async Task<ActionResult> ListarFornecedores()
        {
            var model = (await _intranetBLL.BuscaFornecedores())
                                            .Select(c => new FornecedorModel(c))
                                                .ToList();
            return View("Fornecedores/Listar", model);
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

    }
}