using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CapaDatos.Repositorios
{
    public abstract class CRepositorioConexion
    {
        private readonly string ConexionString;
        public CRepositorioConexion()
        {
            ConexionString = ConfigurationManager.ConnectionStrings["ConexionSistemaAdmision"].ToString();
        }
        protected SqlConnection obtenerConexion()
        {
            return new SqlConnection(ConexionString);
        }
    }
}
