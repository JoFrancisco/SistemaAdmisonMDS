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
        private string contrasenia;
        public CRepositorioPostulante()
        {
            entidad = new E_Postulante();
        }
        public string Contrasenia { set { contrasenia = value;} }
        public int Agregar(E_Postulante CEntidad)
        {
            entidad = CEntidad;
            string agregar = "insert into TPostulante (dni, nombres, apPaterno, apMaterno, fecha) " +
                            "values(@dni, @nombres, @apPaterno, @apMaterno, @fecha)";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", entidad.dni));
            Parametros.Add(new SqlParameter("@nombres", entidad.nombres));
            Parametros.Add(new SqlParameter("@apPaterno", entidad.apPaterno));
            Parametros.Add(new SqlParameter("@apMaterno", entidad.apMaterno));
            Parametros.Add(new SqlParameter("@fecha", entidad.fecha));
            int Agregad = ExecuteNonQuery(agregar);
            string sql = "insert into TLogin (dni, nombreUsuario, contrasenia) " +
                "values(@dni, @nombreUsuario, @contrasenia)";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", entidad.dni));
            Parametros.Add(new SqlParameter("@nombreUsuario", entidad.dni));
            Parametros.Add(new SqlParameter("@contrasenia", contrasenia));
            return (ExecuteNonQuery(sql) == 1 && Agregad == 1) ? 1 : 0 ;
        }

        public bool Buscar(string cad, string nro)
        {
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", cad));
            string Consulta = "select * from TPostulante where dni = @dni";
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
            string editar = "update TPostulante set nombres = @nombres, apPaterno = @apPaterno, " +
                "apMaterno = @apMaterno, fecha = @Fecha where dni = @dni";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", entidad.dni));
            Parametros.Add(new SqlParameter("@nombres", entidad.nombres));
            Parametros.Add(new SqlParameter("@apPaterno", entidad.apPaterno));
            Parametros.Add(new SqlParameter("@apMaterno", entidad.apMaterno));
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

        public bool validarRecibo(string pDni, string pRecibo)
        {
            string validar = "select dni, recibo from TUsuario where (dni = @dni and recibo = @recibo)";
            Parametros = new List<SqlParameter>();
            Parametros.Add(new SqlParameter("@dni", pDni));
            Parametros.Add(new SqlParameter("@recibo", pRecibo));
            var resultado = ExecuteReader(validar);
            foreach (DataRow item in resultado.Rows)
            {
                return item[0].ToString().Equals(pDni.ToString());
            }
            return false;
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
                    apPaterno = item["apPaterno"].ToString(),
                    apMaterno = item["apMaterno"].ToString(),
                    fecha = (DateTime)item["fecha"]
                };
                listaPostulantes.Add(postulante);
            }
            return listaPostulantes;
        }

    }

}
