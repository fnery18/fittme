using Dapper;
using Intranet.Fittme.DAL.Interfaces;
using Intranet.Fittme.MOD;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Fittme.DAL
{
    public class ProdutoDAL : IProdutoDAL
    {
        #region VENDA
        public async Task<ProdutoViewMOD> BuscaProduto(string codigoProduto)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                const string query = @"
                                SELECT 
                                    F.Nome AS 'Fornecedor', P.CodigoProduto, P.Nome, 
                                    P.Imagem AS 'NomeArquivo', P.Quantidade, P.PrecoVenda, 
                                    T.Nome AS 'Tamanho', C.Cor FROM Produtos AS P
                                INNER JOIN Tipos AS T ON P.CodigoTipo = T.Codigo
                                INNER JOIN Cores AS C ON P.CodigoCor = C.Codigo
                                INNER JOIN Fornecedores AS F ON P.CodigoFornecedor = F.Codigo
                                WHERE 
                                    P.CodigoProduto = @codigoProduto";

                return await connection.QueryFirstOrDefaultAsync<ProdutoViewMOD>(query, new { codigoProduto });
            }
        }
        #endregion

        #region PRODUTO
        public async Task<int> CadastraProduto(ProdutoMOD produto)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            INSERT INTO 
                                Produtos (CodigoProdutoFornecedor,CodigoProduto,
                                            CodigoCor,Nome,CodigoTipo,Imagem,
                                            CodigoFornecedor,PrecoCusto,PrecoNota,
                                            PrecoVenda,Quantidade)

                            VALUES (@CodigoProdutoFornecedor,@CodigoProduto,
                                        @CodigoCor,@Nome,@CodigoTipo,@NomeArquivo,
                                        @CodigoFornecedor,@PrecoCusto,@PrecoNota,
                                        @PrecoVenda,@Quantidade)";

                return (await connection.ExecuteAsync(query, produto));
            }
        }
        public async Task<List<ProdutoViewMOD>> BuscaProdutos()
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                               SELECT
	                                forn.Nome as Fornecedor, prod.CodigoProduto, prod.CodigoProdutoFornecedor, prod.Nome, 
	                                prod.Quantidade, CASE WHEN prod.Quantidade > 0 THEN 1 ELSE 0 END as EmEstoque,
	                                tam.Nome as Tamanho, prod.Imagem as NomeArquivo, prod.PrecoCusto, 
	                                prod.PrecoNota, prod.PrecoVenda, cor.Nome as Cor, cor.Cor as CorHexadecimal
                                FROM 
	                                Produtos as prod
                                JOIN 
	                                Fornecedores as forn
                                ON 
	                                prod.CodigoFornecedor = forn.Codigo
                                JOIN
	                                Cores as cor
                                ON
	                                prod.CodigoCor = cor.Codigo
                                JOIN 
	                                Tipos as tam
                                ON 
	                                prod.CodigoTipo = tam.Codigo";

                return (await connection.QueryAsync<ProdutoViewMOD>(query)).ToList();
            }
        }
        public async Task<ProdutoViewMOD> BuscaDetalhesProduto(string codigoProduto)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
								SELECT
	                                forn.Nome as Fornecedor, prod.CodigoProduto, prod.CodigoProdutoFornecedor, prod.Nome, 
	                                prod.Quantidade, CASE WHEN prod.Quantidade > 0 THEN 1 ELSE 0 END as EmEstoque,
	                                prod.Imagem as NomeArquivo, cor.Nome as Cor, cor.Cor as CorHexadecimal,
	                                prod.PrecoNota, prod.PrecoVenda, prod.PrecoCusto
                                FROM 
	                                Produtos as prod
                                JOIN 
	                                Fornecedores as forn
                                ON 
	                                prod.CodigoFornecedor = forn.Codigo
                                JOIN
	                                Cores as cor
                                ON
	                                prod.CodigoCor = cor.Codigo
                                JOIN 
	                                Tipos as tam
                                ON 
	                                prod.CodigoTipo = tam.Codigo
								WHERE
									prod.CodigoProduto = @codigoProduto";

                return await connection.QueryFirstOrDefaultAsync<ProdutoViewMOD>(query, new { codigoProduto });
            }
        }

        public async Task<List<TipoMOD>> BuscaTamanhosDisponiveis(string nomeProduto, string fornecedor)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                                SELECT 
                                    Tipos.Nome, Produtos.Quantidade 
                                FROM 
                                    Produtos
                                JOIN 
                                    Tipos on Produtos.CodigoTipo = Tipos.Codigo
                                JOIN
                                    Fornecedores on Produtos.CodigoFornecedor = Fornecedores.Codigo
                                WHERE 
                                    Produtos.Nome = @nomeProduto AND Fornecedores.Nome = @fornecedor";
                return (await connection.QueryAsync<TipoMOD>(query, new { nomeProduto, fornecedor })).ToList();

            }

        }
        public async Task<List<CorMOD>> BuscaCoresDisponiveis(string nomeProduto, string fornecedor)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                                SELECT 
                                   Cores.Nome, Cores.Cor, Produtos.Quantidade 
                                FROM 
                                    Produtos
                                JOIN 
                                    Cores on Produtos.CodigoCor = Cores.Codigo
                                JOIN
                                    Fornecedores on Produtos.CodigoFornecedor = Fornecedores.Codigo
                                WHERE 
                                    Produtos.Nome = @nomeProduto AND Fornecedores.Nome = @fornecedor";
                return (await connection.QueryAsync<CorMOD>(query, new { nomeProduto, fornecedor })).ToList();

            }
        }

        public async Task<bool> ExcluiProduto(string codigoProduto)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"DELETE FROM Produtos WHERE CodigoProduto = @codigoProduto";
                return (await connection.ExecuteAsync(query, new { codigoProduto })) > 0;
            }
        }

        public async Task<bool> AlteraProduto(ProdutoViewMOD produto)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                                UPDATE 
	                                Produtos 
                                SET 
	                                Nome = @Nome, PrecoCusto = @PrecoCusto, PrecoNota = @PrecoNota, PrecoVenda = @PrecoVenda 
                                WHERE 
	                                CodigoProduto = @CodigoProduto";

                return (await connection.ExecuteAsync(query, produto)) > 0;
            }
        }
        #endregion
    }
}
