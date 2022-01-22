using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Soporte
{
    public class ValidacionDatos
    {
        private ValidationContext Contexto;
        private List<ValidationResult> results;
        private bool Valido;
        private List<string> Mensaje;
        public ValidacionDatos(object instancia)
        {
            Contexto = new ValidationContext(instancia);
            results = new List<ValidationResult>();
            Valido = Validator.TryValidateObject(instancia, Contexto, results, true);
        }
        public bool Validacion()
        {
            if (Valido == false)
            {
                Mensaje = new List<string>();
                foreach (ValidationResult item in results)
                {
                    Mensaje.Add(item.ErrorMessage);
                }
            }
            return Valido;
        }
        public List<string> mensajes()
        {
            return Mensaje;
        }
    }
}
