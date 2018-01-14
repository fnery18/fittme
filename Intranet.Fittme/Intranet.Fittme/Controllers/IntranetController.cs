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

        public ActionResult Cadastrar()
        {
            return View("Produtos/Cadastrar");
        }
    }
}