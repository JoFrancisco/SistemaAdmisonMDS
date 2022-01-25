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
    public class CRepositorioPostulante : CRepositorioGeneral, I_RepositorioPostulante
    {
        E_Postulante entidad;
        public CRepositorioPostulante()
        {
            entidad = new E_Postulante();
        }
        public int Agregar(E_Postulante CEntidad)
        {
            entidad = CEntidad;
            string agregar = "INSERT INTO TPostulante (dni, nombres, fecha) " +
                            "VALUES(@dni, @nombres, @fecha)";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", entidad.dni ));
            Parametros.Add(new SqlParameter("@nombres", entidad.nombres));
            Parametros.Add(new SqlParameter("@fecha", entidad.fecha));
            return ExecuteNonQuery(agregar);
        }

        public bool Buscar(string cad, string nro)
        {
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", cad));
            string Consulta = "select dni from TPostulante where dni = @dni";
            var resultado = ExecuteReader(Consulta);
            foreach (DataRow item in resultado.Rows)
            {
                return item[0].ToString().Equals(cad.ToString());
            }
            return false;
        }

        public int Editar(E_Postulante CEntidad)
        {
            entidad = CEntidad;
            string editar = "update TPostulante set nombres = @nombres, fecha = @Fecha where dni = @dni";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", entidad.dni));
            Parametros.Add(new SqlParameter("@nombres", entidad.nombres));
            Parametros.Add(new SqlParameter("@fecha", entidad.fecha));
            return ExecuteNonQuery(editar);
        }

        public int Eliminar(string CodPk)
        {
            string eliminar = "delete from TPostulante where dni = @dni";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", CodPk));
            return ExecuteNonQuery(eliminar);
        }

        public IEnumerable<E_Postulante> ObtenerTodo()
        {
            string registros = "select * from TPostulante";
            var tablaPostulantes = ExecuteReader(registros);
            var listaPostulantes = new List<E_Postulante>();
            foreach (DataRow item in tablaPostulantes.Rows)
            {
                var postulante = new E_Postulante {
                    dni = item["dni"].ToString(),
                    nombres = item["nombres"].ToString(),
                    fecha = (DateTime)item["fecha"]
                };
                listaPostulantes.Add(postulante);
            }
            return listaPostulantes;
        }

    }

}
