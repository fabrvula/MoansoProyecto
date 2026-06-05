using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaLogica
{
    public class logProforma
    {
        datProforma obj = new datProforma();

        public List<entProforma> Listar()
        {
            return obj.Listar();
        }

        public bool Registrar(entProforma o)
        {
            return obj.Registrar(o);
        }

        public bool CambiarEstado(int id, string estado)
        {
            return obj.CambiarEstado(id, estado);
        }

        public entProforma ObtenerPorId(int id)
        {
            return obj.ObtenerPorId(id);
        }
    }
}
