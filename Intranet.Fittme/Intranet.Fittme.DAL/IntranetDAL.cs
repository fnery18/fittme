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
        //using (var connection = ConnectionFactory.site_fittme())
        //{
        // var query = @"";
        //}

        #region Fornecedor
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
        public async Task<int> AlteraFornecedor(FornecedorMOD fornecedor)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            UPDATE 
                                Fornecedores 
                            SET 
                                Nome = @Nome, Email = @Email, Celular = @Celular
                            WHERE
                                Codigo = @Codigo";
                return await connection.ExecuteAsync(query, new
                {
                    Nome = fornecedor.Nome,
                    Email = fornecedor.Email,
                    Celular = fornecedor.Celular,
                    Codigo = fornecedor.Codigo
                });
            }
        }
        public async Task<int> ExcluiFornecedor(int codigo)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"DELETE FROM Fornecedores WHERE Codigo = @codigo";
                return await connection.ExecuteAsync(query, new { codigo });
            }
        }
        #endregion

        #region Produto
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
        #endregion

        #region Configuracoes
        public async Task<PropriedadesMOD> BuscaPropriedades()
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                PropriedadesMOD propriedades = new PropriedadesMOD();
                var query = @"SELECT * FROM Tipos";
                var query2 = @"SELECT * FROM Fornecedores";
                var query3 = @"SELECT * FROM Cores";
                propriedades.Tipos = (await connection.QueryAsync<TipoMOD>(query)).ToList();
                propriedades.Fornecedores = (await connection.QueryAsync<FornecedorMOD>(query2)).ToList();
                propriedades.Cores = (await connection.QueryAsync<CorMOD>(query3)).ToList();
                return propriedades;
            }
        }
        //Cor
        public async Task<int> CadastraCor(CorMOD cor)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            INSERT INTO 
                                Cores (Nome, Codigo_Cor, Cor) 
                            VALUES  
                                (@Nome, @CodigoCor, @Cor)";

                return await connection.ExecuteAsync(query, new
                {
                    Nome = cor.Nome,
                    CodigoCor = cor.Codigo_Cor,
                    Cor = cor.Cor
                });
            }
        }
        public async Task<int> AlteraCor(CorMOD cor)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            UPDATE 
                                Cores 
                            SET 
                                Nome = @Nome, Codigo_Cor = @Codigo_Cor, Cor = @Cor 
                            WHERE 
                                Codigo = @Codigo";

                return await connection.ExecuteAsync(query, new
                {
                    Nome = cor.Nome,
                    Codigo_Cor = cor.Codigo_Cor,
                    Cor = cor.Cor,
                    Codigo = cor.Codigo
                });
            }
        }
        public async Task<int> ExcluiCor(int codigo)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            DELETE FROM 
                                Cores 
                            WHERE 
                                Codigo = @codigo";

                return await connection.ExecuteAsync(query, new { codigo });
            }
        }

        //Tipo
        public async Task<int> CadastraTipo(TipoMOD tipo)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            INSERT INTO 
                                Tipos (Nome) 
                            VALUES (@Nome)";

                return await connection.ExecuteAsync(query, new { Nome = tipo.Nome });
            }
        }
        public async Task<int> AlteraTipo(TipoMOD tipo)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            UPDATE 
                                Tipos 
                            SET 
                                Nome = @Nome 
                            WHERE Codigo = @Codigo";

                return await connection.ExecuteAsync(query, new { Nome = tipo.Nome, Codigo = tipo.Codigo });
            }
        }

        public async Task<int> ExcluiTipo(int codigo)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"
                            DELETE FROM 
                                Tipos 
                            WHERE 
                                Codigo = @codigo";

                return await connection.ExecuteAsync(query, new { codigo });
            }
        }
        #endregion
    }
}
