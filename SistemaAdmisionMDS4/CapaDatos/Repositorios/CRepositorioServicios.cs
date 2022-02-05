using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositorios
{
    public class CRepositorioServicios:CRepositorioGeneral
    {
        public DataTable Solucionario()
        {
            string registros = "select * from TSolucionario";
            DataTable tablaPostulantes = ExecuteReader(registros);
            return tablaPostulantes;
        }
        public int InsertarNota(string dni, int nota)
        {
            string sql = "insert into TNota (dni, nota) values(@dni, @nota)";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", dni));
            Parametros.Add(new SqlParameter("@nota", nota));
            return ExecuteNonQuery(sql);
        }
        public bool ExistePostulante(string dni)
        {
            string registros = "select * from TPostulante where dni = @dni";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", dni));
            DataTable tablaPostulantes = ExecuteReader(registros);
            return tablaPostulantes.Rows.Count == 1;
        }
        public DataTable Notas()
        {
            string sql = "select T.dni, T.nombres, T.apPaterno, T.apMaterno, N.nota from TPostulante T inner join TNota N on (T.dni = N.dni)";
            return ExecuteReader(sql);
        }
    }
}
