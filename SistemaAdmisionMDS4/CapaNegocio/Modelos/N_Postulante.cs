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
        #region atributos
        private string dni;
        private string nombres;
        private string apPaterno;
        private string apMaterno;
        private DateTime fecha;
        private string aRecibo;
        private List<N_Postulante> n_Postulantes;
        private CRepositorioPostulante repositorioPostulante;
        private List<string> mensajes;
        public EstadoEntidad Estado { private get; set; }
        #endregion atributos

        #region Validacion Getters y Setters
        [Required(ErrorMessage = "dni. campo obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El dni solo debe contener numeros")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "El dni debe tener 8 digitos")]
        public string Dni { get => dni; set => dni = value; }

        [Required(ErrorMessage = "nombres. campo obligatorio")]
        [RegularExpression("[a-z A-Z]+$", ErrorMessage = "Los nombres solo debe contener letras")]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "Los nombres debe contener de 4 a 40 letras")]
        public string Nombres { get => nombres; set => nombres = value; }
       
        [Required(ErrorMessage = "apellido paterno. campo obligatorio")]
        [RegularExpression("[a-z A-Z]+$", ErrorMessage = "El apellido paterno solo debe contener letras")]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "El apellido paterno debe contener de 3 a 40 letras")]
        public string ApPaterno { get => apPaterno; set => apPaterno = value; }

        [Required(ErrorMessage = "apellido materno. campo obligatorio")]
        [RegularExpression("[a-z A-Z]+$", ErrorMessage = "El apellido materno solo debe contener letras")]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "El apellido materno debe contener de 3 a 40 letras")]
        public string ApMaterno { get => apMaterno; set => apMaterno = value; }

        [Required(ErrorMessage = "La fecha de nacimiento es necesario")]
        public DateTime Fecha { get => fecha; set => fecha = value; }

        [Required(ErrorMessage = "recibo. campo obligatorio")]
        [RegularExpression("R-([0-9]){5,5}$", ErrorMessage = "El recibo debe ser R-XXXXX")]
        public string Recibo { get => aRecibo; set => aRecibo = value; }
        #endregion Validaciones Getters y Setters

        public N_Postulante():base(){
            repositorioPostulante = new CRepositorioPostulante();
        }
        public string GuardarCambios(string Contrasenia)
        {
            string Mensaje = null;
            try
            {
                var modeloDatoPostulante = new E_Postulante();
                List<dynamic> Values = new List<dynamic>();
                modeloDatoPostulante.dni = dni;
                modeloDatoPostulante.nombres = nombres;
                modeloDatoPostulante.apPaterno = apPaterno;
                modeloDatoPostulante.apMaterno = apMaterno;
                modeloDatoPostulante.fecha = fecha;
                switch (Estado)
                {
                    case EstadoEntidad.Agregad:
                        repositorioPostulante.Contrasenia = Contrasenia;
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
            foreach (E_Postulante item in modeloDatoPostulante)
            {
                n_Postulantes.Add(new N_Postulante
                {
                    dni = item.dni,
                    nombres = item.nombres,
                    Fecha = item.fecha
                });
            }
            return n_Postulantes;
        }
        public bool Validacion()
        {
            ValidacionDatos validacionDatos = new ValidacionDatos(this);
            bool validate = validacionDatos.Validacion();
            mensajes = validacionDatos.mensajes();
            return validate;
        }

        #region mensajes de error de cada atributo
        private List<string> contains(List<string> ls, string verf)
        {
            List<string> nls = new List<string>();
            if(ls != null)
            {
                foreach (string s in ls)
                {
                    if (s.Contains(verf))
                    {
                        nls.Add(s);
                    }
                }
            }
            return nls;
        }
        public List<string> mensajesDni()
        {
            return contains(mensajes, "dni");
        }
        public List<string> mensajesNombres()
        {
            return contains(mensajes, "nombres");
        }
        public List<string> mensajesApPaterno()
        {
            return contains(mensajes, "apellido paterno");
        }
        public List<string> mensajesApMaterno()
        {
            return contains(mensajes, "apellido materno");
        }
        public List<string> mensajesRecibo()
        {
            return contains(mensajes, "recibo");
        }

        public bool BuscarPostulante(string dni)
        {
            return repositorioPostulante.Buscar(dni, null);
        }
        public bool VerificarRecibo()
        {
            return repositorioPostulante.validarRecibo(dni, aRecibo);
        }
        #endregion
        public void Dispose()
        {

        }
    }
}
