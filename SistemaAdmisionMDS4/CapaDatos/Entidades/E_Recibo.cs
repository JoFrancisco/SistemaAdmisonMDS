using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public class E_Recibo:E_Entidad
    {
        public E_Recibo() : base("TRecibo")
        {

        }
        public override string[] NombresAtributos()
        {
            return new String[] { "nroRecibo", "dni" };
        }
    }
}
