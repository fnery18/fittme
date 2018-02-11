using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intranet.Fittme.Models
{
    public class AutenticacaoModel
    {
        public AutenticacaoModel(AutenticacaoMOD autenticacao)
        {
            Usuario = autenticacao.Usuario;
            Senha = autenticacao.Senha;
        }
        public AutenticacaoModel() { }
        public static implicit operator AutenticacaoModel(AutenticacaoMOD autenticacao)
        {
            return new AutenticacaoModel(autenticacao);
        }

        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Senha { get; set; }
        public bool Administrador { get; set; }
        public string Mensagem { get; set; }
    }
}