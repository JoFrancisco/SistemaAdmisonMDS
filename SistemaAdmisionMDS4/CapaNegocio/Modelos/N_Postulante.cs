using CapaDatos.Contratos;
using CapaDatos.Entidades;
using CapaDatos.Repositorios;
using CapaNegocio.ObjetosValores;
using CapaNegocio.Soporte;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Modelos
{
    public class N_Postulante:IDisposable
    {
        private string dni;
        private string nombres;
        private DateTime fecha;
        private List<N_Postulante> n_Postulantes;
        private I_RepositorioEntidad repositorioPostulante;
        private List<string> mensajes;
        public EstadoEntidad Estado { private get; set; }
        [Required(ErrorMessage = "El dni del postulante es necesario")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El dni del Postulante solo debe contener numeros")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "El dni del Postulante debe tener 8 digitos")]
        public string Dni { get => dni; set => dni = value; }
        [Required(ErrorMessage = "Los nombres del Postulante es necesario")]
        [RegularExpression("[a-z A-Z]+$", ErrorMessage = "Los nombres del Postulante solo debe contener letras")]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "Los nombres del Postulante debe contener de 4 a 40 letras")]
        public string Nombres { get => nombres; set => nombres = value; }
        [Required(ErrorMessage = "La fecha de nacimiento es necesario")]
        public DateTime Fecha { get => fecha; set => fecha = value; }   
        public N_Postulante():base(){
            repositorioPostulante = new CRepositorioPostulante();
        }
        public string GuardarCambios()
        {
            string Mensaje = null;
            try
            {
                var modeloDatoPostulante = new E_Postulante();
                List<dynamic> Values = new List<dynamic>();
                Values.Add(dni);
                Values.Add(nombres);
                Values.Add(fecha);
                modeloDatoPostulante.Valores = Values;

                switch (Estado)
                {
                    case EstadoEntidad.Agregad:
                        repositorioPostulante.Agregar(modeloDatoPostulante);
                        Mensaje = "Registrado correctamente";
                        break;
                    case EstadoEntidad.Eliminad:
                        repositorioPostulante.Eliminar(dni);
                        Mensaje = "Eliminado correctamente";
                        break;
                    case EstadoEntidad.Modificad:
                        int FilasAfectadas = repositorioPostulante.Editar(modeloDatoPostulante);
                        if (FilasAfectadas == 1)
                        {
                            Mensaje = "Editado Correctamente";
                        }
                        else
                        {
                            Mensaje = "No se Realizaron cambios";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Data.SqlClient.SqlException exceptionSql = ex as System.Data.SqlClient.SqlException;
                if (exceptionSql != null && exceptionSql.Number == 2627)
                {
                    Mensaje = "Registro Duplicado";
                }
                else
                {
                    if (exceptionSql != null && exceptionSql.Number == 547)
                    {
                        Mensaje = "INSERT en conflicto con la restricción FOREIGN KEY";
                    }
                    else
                    {
                        Mensaje = exceptionSql.ToString();
                    }
                }
            }
            return Mensaje;
        }
        public IEnumerable<N_Postulante> BuscarPorCodigo(string pFitro)
        {
            return n_Postulantes.FindAll(e => e.dni.Contains(pFitro));
        }
        public IEnumerable<N_Postulante> ObtenerTodo()
        {
            var modeloDatoPostulante = repositorioPostulante.ObtenerTodo();
            n_Postulantes = new List<N_Postulante>();
            foreach (E_Entidad item in modeloDatoPostulante)
            {
                List<dynamic> values = item.Valores;
                n_Postulantes.Add(new N_Postulante
                {
                    dni = values[0],
                    nombres = values[1],
                    Fecha = values[2]
                });
            }
            return n_Postulantes;
        }
        private bool Validacion()
        {
            ValidacionDatos validacionDatos = new ValidacionDatos(this);
            bool validate = validacionDatos.Validacion();
            mensajes = validacionDatos.mensajes();
            return validate;
        }
        public void mensajesDni()
        {

        }
        public void Dispose()
        {

        }
    }
}
