using SevenSuite.Entities;
using SeventSuite.DAL.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SeventSuite.DAL
{
    public class ClienteDAL
    {
        public int Upsert(Cliente cliente)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", cliente.Id == 0 ? (object)DBNull.Value : cliente.Id),
                new SqlParameter("@Cedula", cliente.Cedula),
                new SqlParameter("@Nombre", cliente.Nombre),
                new SqlParameter("@Genero", cliente.Genero),
                new SqlParameter("@FechaNac", cliente.FechaNac),
                new SqlParameter("@EstadoCivilId", cliente.EstadoCivilId)
            };

            return StoredProcedureHelper.ExecuteScalar(
                "dbo.sp_SEVECLIE_Upsert",
                parameters);
        }

        public List<Cliente> Search(string cedula, string nombre)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Cedula",
                    string.IsNullOrWhiteSpace(cedula) ? (object)DBNull.Value : cedula),

                new SqlParameter("@Nombre",
                    string.IsNullOrWhiteSpace(nombre) ? (object)DBNull.Value : nombre)
            };

            var result = new List<Cliente>();
            SqlConnection cn;

            using (var rd = StoredProcedureHelper.ExecuteReader(
                "dbo.sp_SEVECLIE_Search",
                parameters,
                out cn))
            {
                using (cn)
                {
                    while (rd.Read())
                        result.Add(MapCliente(rd));
                }
            }

            return result;
        }

        public Cliente GetById(int id)
        {
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@Id", id)
    };

            SqlConnection cn;

            using (var rd = StoredProcedureHelper.ExecuteReader(
                "dbo.sp_SEVECLIE_GetById",
                parameters,
                out cn))
            {
                using (cn)
                {
                    if (!rd.Read())
                        return null;

                    return MapCliente(rd);
                }
            }
        }


        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@Id", id)
    };

            StoredProcedureHelper.ExecuteNonQuery(
                "dbo.sp_SEVECLIE_Delete",
                parameters);
        }

        private Cliente MapCliente(SqlDataReader rd)
        {
            return new Cliente
            {
                Id = rd.GetInt32(rd.GetOrdinal("Id")),
                Cedula = rd.GetString(rd.GetOrdinal("Cedula")),
                Nombre = rd.GetString(rd.GetOrdinal("Nombre")),
                Genero = rd.GetString(rd.GetOrdinal("Genero")),
                FechaNac = rd.GetDateTime(rd.GetOrdinal("FechaNac")),
                EstadoCivilId = rd.GetInt32(rd.GetOrdinal("EstadoCivilId")),
                EstadoCivil = rd.GetString(rd.GetOrdinal("EstadoCivil"))
            };
        }

        public  DataTable GetReporte(string cedula, string nombre)
        {
            var parameters = new List<SqlParameter>
    {
        new SqlParameter("@Cedula",
            string.IsNullOrWhiteSpace(cedula) ? (object)DBNull.Value : cedula),

        new SqlParameter("@Nombre",
            string.IsNullOrWhiteSpace(nombre) ? (object)DBNull.Value : nombre)
    };

            SqlConnection cn;

            using (var rd = StoredProcedureHelper.ExecuteReader(
                "dbo.sp_SEVECLIE_Reporte",
                parameters,
                out cn))
            {
                using (cn)
                {
                    var dt = new DataTable();
                    dt.Load(rd); 
                    return dt;
                }
            }
        }

    }
}
