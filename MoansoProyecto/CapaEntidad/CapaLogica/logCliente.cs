using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaLogica
{
    public class logCliente
    {
        datCliente obj = new datCliente();

        public List<entCliente> Listar()
        {
            return obj.Listar();
        }

        public bool Registrar(entCliente o)
        {
            return obj.Registrar(o);
        }

        public bool Modificar(entCliente o)
        {
            return obj.Modificar(o);
        }

        public bool Deshabilitar(int id)
        {
            return obj.Deshabilitar(id);
        }
    }
}
