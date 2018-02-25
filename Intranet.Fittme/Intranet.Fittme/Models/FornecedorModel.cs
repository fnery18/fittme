using Intranet.Fittme.MOD;
using System.ComponentModel.DataAnnotations;

namespace Intranet.Fittme.Models
{
    public class FornecedorModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo nome obrigatório."),
            MaxLength(100, ErrorMessage = "Ops! Limite de 100 caracteres para o nome.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo e-mail obrigatório."),
            RegularExpression(@"([\w\.\-_]+)?\w+@[\w-_]+(\.\w+){1,}", ErrorMessage = "Formato do e-mail inválido."),
            MaxLength(100, ErrorMessage = "Ops! Limite de 100 caracteres para o e-mail.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo celular obrigatório."),
            RegularExpression(@"^\([1-9]{2}\) [2-9][0-9]{3,4}\-[0-9]{4}$", ErrorMessage = "Formato do celular inválido."),
            MaxLength(15, ErrorMessage = "Ops! Limite de 15 caracteres para o celular.")]
        public string Celular { get; set; }


        public FornecedorModel() { }
        public FornecedorModel(FornecedorMOD fornecedor)
        {
            Codigo = fornecedor.Codigo;
            Nome = fornecedor.Nome;
            Email = fornecedor.Email;
            Celular = fornecedor.Celular;
        }
    }
}