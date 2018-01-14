using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Index()
        {
            AutenticacaoModel model = await _autenticacaoBLL.Teste();
            return View(model);
        }
    }
}