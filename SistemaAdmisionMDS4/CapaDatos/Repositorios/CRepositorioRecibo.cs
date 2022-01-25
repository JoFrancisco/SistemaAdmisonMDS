using CapaDatos.Contratos;
using CapaDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Repositorios
{
    public class CRepositorioRecibo:CRepositorioGeneral, I_RepositorioRecibo
    {
        E_Recibo entidad;
        public CRepositorioRecibo()
        {
            entidad = new E_Recibo();
        }
        public int Agregar(E_Recibo CEntidad)
        {
            entidad = CEntidad;
            string agregar = "INSERT INTO TRecibo (nroRecibo, dni) " +
                            "VALUES('@nroRecibo', '@dni')";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter($"@nroRecibo", entidad.nroRecibo));
            Parametros.Add(new SqlParameter($"@dni", entidad.dni));
            return ExecuteNonQuery(agregar);
        }

        public int Editar(E_Recibo CEntidad)
        {
            entidad = CEntidad;
            string editar = "update TRecibo set nroRecibo = @nroRecibo where dni = @dni";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter($"@nroRecibo", entidad.nroRecibo));
            Parametros.Add(new SqlParameter($"@dni", entidad.dni));
            return ExecuteNonQuery(editar);
        }

        public int Eliminar(string CodPk)
        {
            string eliminar = "delete from TRecibo where dni = @dni";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", CodPk));
            return ExecuteNonQuery(eliminar);
        }

        public IEnumerable<E_Recibo> ObtenerTodo()
        {
            string registros = "select nroRecibo, dni from TRecibo";
            var tablaPostulantes = ExecuteReader(registros);
            var listaPostulantes = new List<E_Recibo>();
            foreach (DataRow item in tablaPostulantes.Rows)
            {
                var recibo = new E_Recibo
                {
                    nroRecibo = item["nroRecibo"].ToString(),
                    dni = item["dni"].ToString()
                };
                listaPostulantes.Add(recibo);
            }
            return listaPostulantes;
        }

        public bool Buscar(string cad, string nro)
        {
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", cad));
            string Consulta = "select nroRecibo, dni from TRecibo where dni = @dni";
            var resultado = ExecuteReader(Consulta);
            foreach (DataRow item in resultado.Rows)
            {
                return item[0].ToString().Equals(nro.ToString());
            }
            return false;
        }
    }
}
