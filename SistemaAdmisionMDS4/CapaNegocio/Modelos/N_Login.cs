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
        [Required(ErrorMessage = "El dni del usuario es necesario")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El dni del usuario debe contener solo numeros")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "El dni del usuario debe tener 8 digitos")]
        public string CodUsuario { get => codUsuario; set => codUsuario = value; }
        [Required(ErrorMessage = "Es Necesario La contraseña")]
        [RegularExpression("(^[a-z 0-9 A-Z]+$)", ErrorMessage = "La contrasenia no debe incluir caracteres especiales")]
        [StringLength(maximumLength: 40, MinimumLength = 6, ErrorMessage = "la contraseña de ser minimo de 5 caracteres y maximo de 40")]
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
