using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logVehiculo
    {
        datVehiculo obj = new datVehiculo();

        public List<entVehiculo> Listar()
        {
            return obj.Listar();
        }

        public bool Registrar(entVehiculo o)
        {
            return obj.Registrar(o);
        }

        public bool Modificar(entVehiculo o)
        {
            return obj.Modificar(o);
        }

        public bool Deshabilitar(int id)
        {
            return obj.Deshabilitar(id);
        }
    }
}
