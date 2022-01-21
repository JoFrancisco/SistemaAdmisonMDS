using CapaDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.ObjetosValores
{
    public class N_Login
    {
        private CRepositorioLogin usuario;
        private string codUsuario;
        private string contrasenia;
        [Required(ErrorMessage = "el email es necesario")]
        [EmailAddress]
        public string CodUsuario { get => codUsuario; set => codUsuario = value; }
        [Required(ErrorMessage = "El nombre del paciente es necesario")]
        [RegularExpression("(^[a-z A-Z]+$)", ErrorMessage = "El nombre del paciente solo debe contener letras")]
        [StringLength(maximumLength: 40, MinimumLength = 3, ErrorMessage = "El nombre del paciente debe contener de 4 a 40 letras")]
        public string Contrasenia { get => contrasenia; set => contrasenia = value; }
        
        public N_Login()
        {
            usuario = new CRepositorioLogin();
        }
        
        public bool ConsultarUsuario(string codUsuario,string contrasenia)
        {
            return usuario.ConsultarUsuario(codUsuario, contrasenia);
        }
        public void Dispose()
        {

        }
    }
}
