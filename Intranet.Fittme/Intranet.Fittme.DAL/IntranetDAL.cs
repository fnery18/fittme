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
    }
}
