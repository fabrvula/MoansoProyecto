using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;


namespace CapaLogica
{
    public class logProveedor
    {
        datProveedor obj = new datProveedor();

        public List<entProveedor> Listar()
        {
            return obj.Listar();
        }

        public bool Registrar(entProveedor o)
        {
            return obj.Registrar(o);
        }

        public bool Modificar(entProveedor o)
        {
            return obj.Modificar(o);
        }

        public bool Deshabilitar(int id)
        {
            return obj.Deshabilitar(id);
        }
    }
}