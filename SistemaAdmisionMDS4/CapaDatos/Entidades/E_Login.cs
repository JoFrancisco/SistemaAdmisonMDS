using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public class E_Login:E_Entidad
    {
        public E_Login() : base("TLogin")
        {

        }
        public override string[] NombresAtributos()
        {
            return new String[] { "id", "codUsuario", "nombreUsuario", "contrasenia", "tipoUsuario" };
        }
    }
}
