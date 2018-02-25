using System.ComponentModel.DataAnnotations;

namespace Intranet.Fittme.Models.Venda
{
    public class VendaModel
    {
        [Required(ErrorMessage = "Por favor informe a quantidade do produto")]
        public int QuantidadeEscolhida { get; set; }
        [Required(ErrorMessage = "Por favor informe o código do produto")]
        public string CodigoProduto { get; set; }
    }
}