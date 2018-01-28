using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intranet.Fittme.MOD;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace Intranet.Fittme.BLL
{
    public class IntranetBLL : IIntranetBLL
    {
        private IIntranetDAL _intranetDAL;
        public IntranetBLL(IIntranetDAL intranetDAL)
        {
            _intranetDAL = intranetDAL;
        }

        public async Task<bool> AlteraFornecedor(FornecedorMOD fornecedor)
        {
            return await _intranetDAL.AlteraFornecedor(fornecedor) > 0;
        }

        public async Task<List<FornecedorMOD>> BuscaFornecedores()
        {
            return await _intranetDAL.BuscaFornecedores();
        }

        public async Task<PropriedadesMOD> BuscaPropriedades()
        {
            return await _intranetDAL.BuscaPropriedades();
        }

        public async Task<bool> CadastraFornecedor(FornecedorMOD fornecedor)
        {
            return await _intranetDAL.CadastraFornecedor(fornecedor) > 0;
        }

        public async Task<bool> CadastraProduto(ProdutoMOD produto)
        {
            var caminho = UploadImagem(produto.Imagem);

            return await _intranetDAL.CadastraProduto(produto, caminho) > 0;
        }

        public async Task<bool> ExcluiFornecedor(int codigo)
        {
            return await _intranetDAL.ExcluiFornecedor(codigo) > 0;
        }

        private string UploadImagem(HttpPostedFileBase imagem)
        {
            var caminho = HostingEnvironment.MapPath("~/Content/Images/Produtos");
            string caminhoArquivo = Path.Combine(caminho, Path.GetFileName(imagem.FileName));

            imagem.SaveAs(caminhoArquivo);

            return imagem.FileName;
        }
    }
}
