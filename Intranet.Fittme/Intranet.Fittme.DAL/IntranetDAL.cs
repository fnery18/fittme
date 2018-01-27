using Intranet.Fittme.DAL.Interfaces;
using Intranet.Fittme.MOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Intranet.Fittme.DAL
{
    public class IntranetDAL : IIntranetDAL
    {
        public async Task<List<FornecedorMOD>> BuscaFornecedores()
        {
            using (var conncetion = ConnectionFactory.site_fittme())
            {
                var query = @"
                            SELECT 
                                * 
                            FROM 
                                Fornecedores";

                return (await conncetion.QueryAsync<FornecedorMOD>(query)).ToList();
            }
        }

        public async Task<PropriedadesMOD> BuscaPropriedades()
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                PropriedadesMOD propriedades = new PropriedadesMOD();
                var query = @"SELECT * FROM Tipos";
                var query2 = @"SELECT * FROM Fornecedores";
                propriedades.Tipos = (await connection.QueryAsync<TipoMOD>(query)).ToList();
                propriedades.Fornecedores = (await connection.QueryAsync<FornecedorMOD>(query2)).ToList();
                return propriedades;
            }
        }

        public async Task<int> CadastraFornecedor(FornecedorMOD fornecedor)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                using (var transation = connection.BeginTransaction())
                {
                    var query = @"
                                INSERT INTO 
                                    Fornecedores 
                                VALUES(
                                    @NOME, @EMAIL, @CELULAR
                                )";

                    var linhasInseridas = await connection.ExecuteAsync(query, new
                    {
                        NOME = fornecedor.Nome,
                        EMAIL = fornecedor.Email,
                        CELULAR = fornecedor.Celular
                    }, transation);

                    if (linhasInseridas > 0)
                        transation.Commit();

                    return linhasInseridas;
                }
            }
        }

        public async Task<int> CadastraProduto(ProdutoMOD produto, string caminho)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            INSERT INTO 
                                Produtos (Codigo_Produto, Nome, 
                                            Codigo_Tipo, Imagem, Codigo_Fornecedor, Preco, Quantidade)
                            VALUES 
                                (@Codigo_Produto, @Nome, @Codigo_Tipo, 
                                    @caminho, @Codigo_Fornecedor, @Preco, @Quantidade)";

                return (await connection.ExecuteAsync(query, new
                {
                    Codigo_Produto = produto.Codigo_Produto,
                    caminho = caminho,
                    Nome = produto.Nome,
                    Codigo_Tipo = produto.Codigo_Tipo,
                    Codigo_Fornecedor = produto.Codigo_Fornecedor,
                    Preco = produto.Preco,
                    Quantidade = produto.Quantidade
                }));

            }
        }
    }
}
