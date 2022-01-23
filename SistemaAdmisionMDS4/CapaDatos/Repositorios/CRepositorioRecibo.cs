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
    public class CRepositorioRecibo:CRepositorioGeneral, I_RepositorioEntidad
    {
        E_Entidad entidad;
        public CRepositorioRecibo()
        {
            entidad = new E_Recibo();
        }
        public int Agregar(E_Entidad CEntidad)
        {
            entidad = CEntidad;
            string agregar = "INSERT INTO TRecibo (nroRecibo, dni) " +
                            "VALUES('@nroRecibo', '@dni')";
            List<dynamic> ParametrosS = entidad.Valores;
            string[] NombresAtributos = entidad.NombresAtributos();
            Parametros = new List<SqlParameter>();
            for (int k = 0; k < NombresAtributos.Length; k++)
            {
                Parametros.Add(new SqlParameter($"@{NombresAtributos[k]}", ParametrosS[k]));
            }
            return ExecuteNonQuery(agregar);
        }

        public int Editar(E_Entidad CEntidad)
        {
            entidad = CEntidad;
            string editar = "update TRecibo set nroRecibo = @nroRecibo where dni = @dni";
            List<dynamic> ParametrosS = entidad.Valores;
            Parametros = new List<System.Data.SqlClient.SqlParameter>();
            for (int k = 0; k < entidad.numeroAtributos(); k++)
            {
                Parametros.Add(new SqlParameter($"@{entidad.NombresAtributos()[k]}", ParametrosS[k]));
            }
            return ExecuteNonQuery(editar);
        }

        public int Eliminar(string CodPk)
        {
            string eliminar = "delete from TRecibo where dni = @dni";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", CodPk));
            return ExecuteNonQuery(eliminar);
        }

        public IEnumerable<E_Entidad> ObtenerTodo()
        {
            string registros = "select nroRecibo, dni from TRecibo";
            var tablaPostulantes = ExecuteReader(registros);
            var listaPostulantes = new List<E_Entidad>();
            List<dynamic> Valores;
            foreach (DataRow item in tablaPostulantes.Rows)
            {
                Valores = new List<dynamic>();
                for (int k = 0; k < entidad.numeroAtributos(); k++)
                {
                    Valores.Add(item[k].ToString());
                }
                entidad.Valores = Valores;
                listaPostulantes.Add(entidad);
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
