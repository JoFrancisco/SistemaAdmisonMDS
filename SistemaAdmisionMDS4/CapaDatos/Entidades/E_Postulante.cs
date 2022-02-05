using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public class E_Postulante
    {
        public string dni { get; set; }
        public string nombres { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public DateTime fecha { get; set; }
    }
}
