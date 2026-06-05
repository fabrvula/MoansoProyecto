using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaLogica
{
    public class logProducto
    {
        datProducto obj = new datProducto();

        public List<entProducto> Listar()
        {
            return obj.Listar();
        }

        public bool Registrar(entProducto o)
        {
            return obj.Registrar(o);
        }

        public bool Modificar(entProducto o)
        {
            return obj.Modificar(o);
        }

        public bool Deshabilitar(int id)
        {
            return obj.Deshabilitar(id);
        }
    }
}