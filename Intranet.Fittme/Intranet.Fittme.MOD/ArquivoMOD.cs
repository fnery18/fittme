using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Intranet.Fittme.MOD
{
    public class ArquivoMOD
    {
        public HttpPostedFileBase Arquivo { get; set; }
        public string Nome { get; set; }
    }
}
