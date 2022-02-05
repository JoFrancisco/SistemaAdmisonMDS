using CapaDatos.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Modelos
{
    public class N_Servicios
    {
        CRepositorioServicios servicios;
        public N_Servicios()
        {
            servicios = new CRepositorioServicios();
        }
        public DataTable Solucionario()
        {
            return servicios.Solucionario();
        }
        public bool InsertarNota(string dni, int nota)
        {
            return servicios.InsertarNota(dni, nota) == 1;
        }
        public bool ValidarPostulante(string dni)
        {
            return servicios.ExistePostulante(dni);
        }
        public DataTable Notas()
        {
            return servicios.Notas();
        }
    }
}
