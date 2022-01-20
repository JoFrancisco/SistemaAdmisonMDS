using CapaDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.ObjetosValores
{
    public class N_Login
    {
        private CRepositorioLogin usuario;
        public N_Login()
        {
            usuario = new CRepositorioLogin();
        }
        public bool existeUsuario(string user, string password)
        {
            return usuario.ConsultarUsuario(user, password);
        }
    }
}
