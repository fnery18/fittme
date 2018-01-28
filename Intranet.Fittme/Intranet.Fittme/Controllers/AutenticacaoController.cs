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
    public class AutenticacaoController : Controller
    {
        private IAutenticacaoBLL _autenticacaoBLL;
        public AutenticacaoController(IAutenticacaoBLL autenticacaoBLL)
        {
            _autenticacaoBLL = autenticacaoBLL;
        }
        // GET: Autenticacao
        public async Task<ActionResult> Login()
        {
            if (Session["user"] == null)
                return View("Index");

            return RedirectToAction("Index", "Intranet");

        }
        [HttpPost]
        public async Task<ActionResult> Autentica(AutenticacaoModel model)
        {
            if (ModelState.IsValid)
            {
                AutenticacaoMOD usuario = new AutenticacaoMOD
                {
                    Senha = model.Senha,
                    Usuario = model.Usuario
                };

                if (await _autenticacaoBLL.ValidaUsuario(usuario))
                {
                    Session["user"] = usuario.Usuario;
                    return RedirectToAction("Index", "Intranet");
                }

            }
            return RedirectToAction("Login");
        }
    }
}