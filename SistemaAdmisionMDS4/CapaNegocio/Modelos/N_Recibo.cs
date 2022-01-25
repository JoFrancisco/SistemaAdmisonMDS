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
    public class N_Recibo
    {
        string nroRecibo;
        string dni;
        private List<N_Recibo> n_Recibos;
        private I_RepositorioRecibo repositorioRecibo;
        private List<string> mensajes;
        public EstadoEntidad Estado { private get; set; }

        [Required(ErrorMessage = "El numero de Recibo del postulante es necesario")]
        [RegularExpression("R-[0123456789]{5,5}", ErrorMessage = "El numero del Recibo del Postulante debe ser R-xxxxx")]
        public string NroRecibo { get => nroRecibo; set => nroRecibo = value; }
        [Required(ErrorMessage = "El dni del postulante es necesario")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El dni del Postulante solo debe contener numeros")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "El dni del Postulante debe tener 8 digitos")]
        public string Dni { get => dni; set => dni = value; }
        public N_Recibo() : base()
        {
            repositorioRecibo = new CRepositorioRecibo();
        }
        public string GuardarCambios()
        {
            string Mensaje = null;
            try
            {
                var modeloRecibo = new E_Recibo();
                modeloRecibo.dni = dni;
                modeloRecibo.nroRecibo = nroRecibo;
                switch (Estado)
                {
                    case EstadoEntidad.Agregad:
                        repositorioRecibo.Agregar(modeloRecibo);
                        Mensaje = "Registrado correctamente";
                        break;
                    case EstadoEntidad.Eliminad:
                        repositorioRecibo.Eliminar(dni);
                        Mensaje = "Eliminado correctamente";
                        break;
                    case EstadoEntidad.Modificad:
                        int FilasAfectadas = repositorioRecibo.Editar(modeloRecibo);
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
        
        public IEnumerable<N_Recibo> ObtenerTodo()
        {
            var modeloRecibo = repositorioRecibo.ObtenerTodo();
            n_Recibos = new List<N_Recibo>();
            foreach (E_Recibo item in modeloRecibo)
            {
                n_Recibos.Add(new N_Recibo
                {
                    nroRecibo = item.nroRecibo,
                    dni = item.dni
                }); ;
            }
            return n_Recibos;
        }
        public bool Validacion()
        {
            ValidacionDatos validacionDatos = new ValidacionDatos(this);
            bool validate = validacionDatos.Validacion();
            mensajes = validacionDatos.mensajes();
            return validate;
        }
        private List<string> contains(List<string> ls, string verf)
        {
            List<string> nls = new List<string>();
            if (ls != null)
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
        public List<string> mensajesRecibo()
        {
            return contains(mensajes, "Recibo");
        }
        public bool buscarRecibo(string dni, string nro)
        {
            return repositorioRecibo.Buscar(dni,nro);
        }
        public void Dispose()
        {

        }
    }
}
