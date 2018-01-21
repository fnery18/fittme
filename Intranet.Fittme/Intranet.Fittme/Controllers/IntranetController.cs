using Intranet.Fittme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intranet.Fittme.Controllers
{
    public class IntranetController : Controller
    {
        //[SessionCheck]
        public ActionResult Index()
        {
            return View();
        }

        #region Produtos
        public ActionResult Produto()
        {
            return View("Produtos/Cadastrar");
        }
        public ActionResult ListarProdutos()
        {
            return View("Produtos/Listar");
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
        public ActionResult ListarFornecedores()
        {
            return View("Fornecedores/Listar");
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