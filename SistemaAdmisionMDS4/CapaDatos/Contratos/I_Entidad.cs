using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Contratos
{
    public interface I_Entidad<Entity> where Entity : class
    {
        int Agregar(Entity CEntidad);
        int Editar(Entity CEntidad);
        int Eliminar(string CodPk);

        bool Buscar(string cad, string nro);
        IEnumerable<Entity> ObtenerTodo();
    }
}
