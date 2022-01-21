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
        private string insertar = "INSERT TLogin (codUsuario, nombreUsuario, contrasenia, tipoUsuario) " +
                                    "VALUES (@codigo, @nombre, @contrasenia, @tipo)";
        public bool ConsultarUsuario(string codUsuario, string contrasenia)
        {
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@codUsuario", codUsuario));
            Parametros.Add(new SqlParameter("@contrasenia", contrasenia));
            string Consulta = "select codUsuario from TLogin where codUsuario = @codUsuario and contrasenia = @contrasenia";
            var resultado = ExecuteReader(Consulta);
            foreach (DataRow item in resultado.Rows)
            {
                return item[0].ToString().Equals(codUsuario.ToString());
            }
            return false;
        }
        public int Agregar(string codigo, string nombre, string contrasenia, string tipo)
        {
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@codigo", codigo));
            Parametros.Add(new SqlParameter("@nombre", nombre));
            Parametros.Add(new SqlParameter("@contrasenia", contrasenia));
            Parametros.Add(new SqlParameter("@tipo", tipo));
            return ExecuteNonQuery(insertar);
        }
    }
}
