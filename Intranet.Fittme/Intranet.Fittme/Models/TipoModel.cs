using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Fittme.Models
{
    public class TipoModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Quantidade { get; set; }
        public TipoModel()
        {

        }
        public TipoModel(TipoMOD tipo)
        {
            Nome = tipo.Nome;
            Quantidade = tipo.Quantidade;
            Codigo = tipo.Codigo;
        }
    }
}