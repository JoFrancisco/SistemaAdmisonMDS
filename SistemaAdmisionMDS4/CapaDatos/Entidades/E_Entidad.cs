using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public abstract class E_Entidad
    {

        private string aNombre_Tabla;

        private string[] aNombresCabezera;
        private List<dynamic> aValores;
        public E_Entidad(string pNombreTabla)
        {
            aNombre_Tabla = pNombreTabla;
            aNombresCabezera = NombresAtributos();
            aValores = null;
        }
        public List<dynamic> Valores { get => aValores; set => aValores = value; }
        public string getCodigo()
        {
            return aValores[0];
        }
        public string Nombre_Tabla()
        {
            return aNombre_Tabla;
        }
        public int numeroAtributos()
        {
            return aNombresCabezera.Length;
        }
        public abstract string[] NombresAtributos();

    }
}
