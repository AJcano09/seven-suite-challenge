using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SeventSuite.DAL.Helper
{
    public static class StoredProcedureHelper
    {
        public static int ExecuteScalar(
            string procedureName,
            IEnumerable<SqlParameter> parameters)
        {
            using (var cn = DbConnection.Create())
            using (var cmd = CreateCommand(cn, procedureName, parameters))
            {
                cn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static void ExecuteNonQuery(
            string procedureName,
            IEnumerable<SqlParameter> parameters)
        {
            using (var cn = DbConnection.Create())
            using (var cmd = CreateCommand(cn, procedureName, parameters))
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static SqlDataReader ExecuteReader(
            string procedureName,
            IEnumerable<SqlParameter> parameters,
            out SqlConnection connection)
        {
            connection = DbConnection.Create();

            var cmd = CreateCommand(connection, procedureName, parameters);
            connection.Open();

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private static SqlCommand CreateCommand(
            SqlConnection cn,
            string procedureName,
            IEnumerable<SqlParameter> parameters)
        {
            var cmd = new SqlCommand(procedureName, cn)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                foreach (var p in parameters)
                    cmd.Parameters.Add(p);
            }

            return cmd;
        }
    }
}
