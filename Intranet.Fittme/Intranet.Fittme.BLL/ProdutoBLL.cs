using Intranet.Fittme.BLL.Interfaces;
using Intranet.Fittme.DAL.Interfaces;
using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Intranet.Fittme.BLL
{
    public class ProdutoBLL : IProdutoBLL
    {
        private IProdutoDAL _produtoDAL;
        private IIntranetDAL _intranetDAL;
        private string caminhoProdutos = HostingEnvironment.MapPath("~/Content/Images/Produtos");
        public ProdutoBLL(IProdutoDAL produtoDAL, IIntranetDAL intranetDAL)
        {
            _produtoDAL = produtoDAL;
            _intranetDAL = intranetDAL;
        }

        #region VENDA
        public async Task<ProdutoViewMOD> BuscaProduto(string codigoProduto, int quantidadeTotalVenda)
        {
            var produto = await _produtoDAL.BuscaProduto(codigoProduto);

            if (produto?.Quantidade >= quantidadeTotalVenda)
            {
                return produto;
            }
            return null;
        }
        #endregion

        #region PRODUTO
        public async Task<bool> CadastraProduto(ProdutoMOD produto)
        {
            FormataAtributos(ref produto);

            return await _produtoDAL.CadastraProduto(produto) > 0;
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
            var produtos = await _produtoDAL.BuscaProdutos();

            return produtos;
        }

        public async Task<ProdutoViewMOD> BuscaDetalhesProduto(string codigoProduto)
        {
            var produto = await _produtoDAL.BuscaDetalhesProduto(codigoProduto);

            if (produto != null)
            {
                produto.CoresDisponiveis = await _produtoDAL.BuscaCoresDisponiveis(produto.Nome, produto.Fornecedor);
                produto.TamanhosDisponiveis = await _produtoDAL.BuscaTamanhosDisponiveis(produto.Nome, produto.Fornecedor);
            }
            return produto;

        }

        public async Task<bool> ExcluirProduto(string codigoProduto)
        {
            return await _produtoDAL.ExcluiProduto(codigoProduto);
        }

        public async Task<bool> AlteraProduto(ProdutoViewMOD produto)
        {
            return await _produtoDAL.AlteraProduto(produto);
        }
        #endregion

        #region FUNÇÕES
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
