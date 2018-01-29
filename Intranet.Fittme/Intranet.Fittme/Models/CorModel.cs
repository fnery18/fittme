using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Fittme.Models
{
    public class CorModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Codigo_Cor { get; set; }
        public string Cor { get; set; }
        public CorModel()
        {

        }
        public CorModel(CorMOD cor)
        {
            Codigo = cor.Codigo;
            Nome = cor.Nome;
            Codigo_Cor = cor.Codigo_Cor;
            Cor = cor.Cor;
        }
    }
}