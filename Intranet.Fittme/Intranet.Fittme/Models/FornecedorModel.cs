using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Intranet.Fittme.Models
{
    public class FornecedorModel
    {
        public int Codigo { get; set; }

        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, RegularExpression(@"([\w\.\-_]+)?\w+@[\w-_]+(\.\w+){1,}"), 
            MaxLength(100)]
        public string Email { get; set; }

        [Required, RegularExpression(@"([1-9]{2}\) [2-9][0-9]{3,4}\-[0-9]{4}$"), 
            MaxLength(15)]
        public string Celular { get; set; }
    }
}