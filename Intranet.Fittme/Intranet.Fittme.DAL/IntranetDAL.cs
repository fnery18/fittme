﻿using Intranet.Fittme.DAL.Interfaces;
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
                                Cores (Nome, CodigoCor, Cor) 
                            VALUES  
                                (@Nome, @CodigoCor, @Cor)";

                return await connection.ExecuteAsync(query, new
                {
                    Nome = cor.Nome,
                    CodigoCor = cor.CodigoCor,
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
                                Nome = @Nome, CodigoCor = @CodigoCor, Cor = @Cor 
                            WHERE 
                                Codigo = @Codigo";

                return await connection.ExecuteAsync(query, new
                {
                    Nome = cor.Nome,
                    CodigoCor = cor.CodigoCor,
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
        public string BuscaCodigoCor(int codigoCor)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"SELECT CodigoCor FROM Cores WHERE Codigo = @codigoCor";
                return connection.QueryFirstOrDefault<string>(query, new { codigoCor });
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

        public string BuscaTipo(int codigo)
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"SELECT Nome FROM Tipos WHERE Codigo = @codigo";
                return connection.QueryFirstOrDefault<string>(query, new { codigo });
            }
        }

        #endregion
    }
}
