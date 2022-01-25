using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositorios
{
    public abstract class CRepositorioGeneral:CRepositorioConexion
    {
        protected List<SqlParameter> Parametros;
        protected int ExecuteNonQuery(string transaccionSql)
        {
            using (var conexion = obtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = transaccionSql;
                    foreach (SqlParameter item in Parametros)
                    {
                        comando.Parameters.Add(item);
                    }
                    int resultado = comando.ExecuteNonQuery();
                    Parametros.Clear();
                    return resultado;
                }
            }
        }
        protected DataTable ExecuteReader(string transaccionSql)
        {
            using (var conexion = obtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = transaccionSql;
                    comando.CommandType = CommandType.Text;
                    if (Parametros != null)
                    {
                        foreach (SqlParameter item in Parametros)
                        {
                            comando.Parameters.Add(item);
                        }
                    }
                    SqlDataReader reader = comando.ExecuteReader();
                    using (var tabla = new DataTable())
                    {
                        tabla.Load(reader);
                        reader.Dispose();
                        return tabla;
                    }
                }

            }
        }
    }
}
