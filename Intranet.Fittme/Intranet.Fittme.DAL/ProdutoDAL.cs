using Dapper;
using Intranet.Fittme.DAL.Interfaces;
using Intranet.Fittme.MOD;
using System.Threading.Tasks;

namespace Intranet.Fittme.DAL
{
    public class ProdutoDAL : IProdutoDAL
    {
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
    }
}
