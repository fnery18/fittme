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
        public string CodigoCor { get; set; }
        public string Cor { get; set; }
        public string Quantidade { get; set; }
        public CorModel()
        {

        }
        public CorModel(CorMOD cor)
        {
            Codigo = cor.Codigo;
            Quantidade = cor.Quantidade;
            Nome = cor.Nome;
            CodigoCor = cor.CodigoCor;
            Cor = cor.Cor;
        }
    }
}