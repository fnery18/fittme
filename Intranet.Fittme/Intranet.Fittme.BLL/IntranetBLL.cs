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
        private string caminhoProdutos = HostingEnvironment.MapPath("~/Content/Images/Produtos");
        private IIntranetDAL _intranetDAL;
        public IntranetBLL(IIntranetDAL intranetDAL)
        {
            _intranetDAL = intranetDAL;
        }

        #region Fornecedor
        public async Task<bool> CadastraFornecedor(FornecedorMOD fornecedor)
        {
            return await _intranetDAL.CadastraFornecedor(fornecedor) > 0;
        }
        public async Task<bool> AlteraFornecedor(FornecedorMOD fornecedor)
        {
            return await _intranetDAL.AlteraFornecedor(fornecedor) > 0;
        }
        public async Task<List<FornecedorMOD>> BuscaFornecedores()
        {
            return await _intranetDAL.BuscaFornecedores();
        }
        public async Task<bool> ExcluiFornecedor(int codigo)
        {
            return await _intranetDAL.ExcluiFornecedor(codigo) > 0;
        }
        #endregion

        #region Produto
        public async Task<bool> CadastraProduto(ProdutoMOD produto)
        {
            FormataAtributos(ref produto);

            return await _intranetDAL.CadastraProduto(produto) > 0;
        }

        private void FormataAtributos(ref ProdutoMOD produto)
        {
            var cor = _intranetDAL.BuscaCodigoCor(produto.CodigoCor);
            var tamanho = _intranetDAL.BuscaTipo(produto.CodigoTipo);
            produto.CodigoProduto = string.Format("{0}-{1}-{2}", produto.CodigoProduto, cor, tamanho);
            produto.NomeArquivo = UploadImagem(produto.Imagem);
        }

        public async Task<List<ProdutoViewMOD>> BuscaProdutos()
        {
            var produtos = await _intranetDAL.BuscaProdutos();

            return produtos;
        }

        public async Task<ProdutoViewMOD> BuscaDetalhesProduto(string codigoProduto)
        {
            var produto = await _intranetDAL.BuscaDetalhesProduto(codigoProduto);

            if (produto != null)
            {
                produto.CoresDisponiveis = await _intranetDAL.BuscaCoresDisponiveis(produto.Nome, produto.Fornecedor);
                produto.TamanhosDisponiveis = await _intranetDAL.BuscaTamanhosDisponiveis(produto.Nome, produto.Fornecedor);
            }
            return produto;

        }
        #endregion

        #region Configuracoes
        public async Task<PropriedadesMOD> BuscaPropriedades()
        {
            return await _intranetDAL.BuscaPropriedades();
        }
        //COR
        public async Task<bool> CadastraCor(CorMOD cor)
        {
            return await _intranetDAL.CadastraCor(cor) > 0;
        }
        public async Task<bool> AlteraCor(CorMOD cor)
        {
            return await _intranetDAL.AlteraCor(cor) > 0;
        }
        public async Task<bool> ExcluiCor(CorMOD cor)
        {
            return await _intranetDAL.ExcluiCor(cor.Codigo) > 0;
        }

        //TIPO
        public async Task<bool> AlteraTipo(TipoMOD tipo)
        {
            return await _intranetDAL.AlteraTipo(tipo) > 0;
        }
        public async Task<bool> CadastraTipo(TipoMOD tipo)
        {
            return await _intranetDAL.CadastraTipo(tipo) > 0;
        }
        public async Task<bool> ExcluiTipo(TipoMOD tipo)
        {
            return await _intranetDAL.ExcluiTipo(tipo.Codigo) > 0;
        }
        #endregion

        #region Funções
        private string UploadImagem(HttpPostedFileBase imagem)
        {
            string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(imagem.FileName);
            string caminhoArquivo = Path.Combine(caminhoProdutos, nomeArquivo);

            imagem.SaveAs(caminhoArquivo);

            return nomeArquivo;
        }


        #endregion

    }
}
