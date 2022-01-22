using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public class E_Postulante:E_Entidad
    {
        public E_Postulante() : base("TPostulante")
        {

        }
        public override string[] NombresAtributos()
        {
            return new String[] { "dni", "nombres", "fecha" };
        }
    }
}
