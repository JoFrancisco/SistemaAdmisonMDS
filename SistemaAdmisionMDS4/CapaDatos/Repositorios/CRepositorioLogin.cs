using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositorios
{
    public class CRepositorioLogin:CRepositorioGeneral
    {
        private string insertar = "insert TLogin (dni, nombreUsuario, contrasenia) " +
                                    "values (@dni, @nombreUsuario, @contrasenia)";
        public bool ConsultarUsuario(string codUsuario, string contrasenia)
        {
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@nombreUsuario", codUsuario));
            Parametros.Add(new SqlParameter("@contrasenia", contrasenia));
            string Consulta = "select L.dni, U.tipoUsuario from TLogin L inner join TUsuario U on L.dni = U.dni " +
                "where (nombreUsuario = @nombreUsuario and contrasenia = @contrasenia)";
            var resultado = ExecuteReader(Consulta);
            foreach (DataRow item in resultado.Rows)
            {
                return item[0].ToString().Equals(codUsuario.ToString());
            }
            return false;
        }
        public bool BuscarCuenta(string dni)
        {
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", dni));
            string Consulta = "select * from TLogin where dni = @dni";
            var resultado = ExecuteReader(Consulta);
            foreach (DataRow item in resultado.Rows)
            {
                return item[0].ToString().Equals(dni.ToString());
            }
            return false;
        }
        public int Agregar(string codigo, string nombre, string contrasenia, string tipo)
        {
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", codigo));
            Parametros.Add(new SqlParameter("@nombreUsuario", nombre));
            Parametros.Add(new SqlParameter("@contrasenia", contrasenia));
            return ExecuteNonQuery(insertar);
        }
        public string tipoUsuario(string nombreUsuario)
        {
            string sql = "select tipoUsuario from TLogin L inner join TUsuario U on L.dni = U.dni where nombreUsuario = @nombreUsuario";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@nombreusuario", nombreUsuario));
            var resultado = ExecuteReader(sql);
            foreach (DataRow item in resultado.Rows)
            {
                return item[0].ToString();
            }
            return null;
        }
    }
}
