using Intranet.Fittme.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Intranet.Fittme.MOD;

namespace Intranet.Fittme.DAL
{
    public class AutenticacaoDAL : IAutenticacaoDAL
    {
        public async Task<AutenticacaoMOD> Teste()
        {
            using (var connection = ConnectionFactory.site_fittme())
            {
                var query = @"SELECT * FROM Usuarios";

                return await connection.QueryFirstOrDefaultAsync<AutenticacaoMOD>(query);
            }
        }
    }
}
