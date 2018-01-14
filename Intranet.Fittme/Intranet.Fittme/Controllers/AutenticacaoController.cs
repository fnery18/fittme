using Intranet.Fittme.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Intranet.Fittme.Controllers
{
    public class AutenticacaoController : Controller
    {
        private IAutenticacaoBLL _autenticacaoBLL;
        public AutenticacaoController(IAutenticacaoBLL autenticacaoBLL)
        {
            _autenticacaoBLL = autenticacaoBLL;
        }
        // GET: Autenticacao
        public ActionResult Index()
        {
            return View();
        }
    }
}