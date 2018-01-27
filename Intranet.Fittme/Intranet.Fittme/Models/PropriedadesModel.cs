using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Fittme.Models
{
    public class PropriedadesModel
    {
        public List<FornecedorModel> Fornecedores { get; set; }
        public List<TipoModel> Tipos { get; set; }

        public PropriedadesModel()
        {

        }

        public PropriedadesModel(PropriedadesMOD prop)
        {
            Fornecedores = prop.Fornecedores.Select(c => new FornecedorModel(c)).ToList();
            Tipos = prop.Tipos.Select(c => new TipoModel(c)).ToList();
        }
    }
}