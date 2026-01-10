using SevenSuite.Entities;
using SeventSuite.DAL.Helper;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SeventSuite.DAL
{
    public class CatalogoDAL
    {
        public List<EstadoCivil> GetEstadosCiviles()
        {
            var result = new List<EstadoCivil>();
            SqlConnection cn;

            using (var rd = StoredProcedureHelper.ExecuteReader(
                "dbo.sp_EstadoCivil_GetAll",
                null,
                out cn))
            {
                using (cn)
                {
                    while (rd.Read())
                    {
                        result.Add(new EstadoCivil
                        {
                            Id = rd.GetInt32(0),
                            Nombre = rd.GetString(1)
                        });
                    }
                }
            }

            return result;
        }
    }
}
