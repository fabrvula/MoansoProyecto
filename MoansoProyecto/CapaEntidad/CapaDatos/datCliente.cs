using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datCliente
    {
        private static List<entCliente> lista = new List<entCliente>()
        {
            new entCliente { IdCliente=1, NombreCliente="Juan", RazonSocial="Juan Pérez S.A.",
                             TipoCliente="Empresa", Ciudad="Trujillo",
                             Activo=true, FechaRegistro=DateTime.Now },
            new entCliente { IdCliente=2, NombreCliente="María", RazonSocial="María García",
                             TipoCliente="Natural", Ciudad="Lima",
                             Activo=true, FechaRegistro=DateTime.Now }
        };
        private static int correlativo = 3;

        public List<entCliente> Listar()
        {
            return lista;
        }

        public bool Registrar(entCliente obj)
        {
            obj.IdCliente = correlativo++;
            lista.Add(obj);
            return true;
        }

        public bool Modificar(entCliente obj)
        {
            var item = lista.Find(x => x.IdCliente == obj.IdCliente);
            if (item == null) return false;
            item.NombreCliente = obj.NombreCliente;
            item.RazonSocial = obj.RazonSocial;
            item.TipoCliente = obj.TipoCliente;
            item.Ciudad = obj.Ciudad;
            item.Activo = obj.Activo;
            return true;
        }

        public bool Deshabilitar(int id)
        {
            var item = lista.Find(x => x.IdCliente == id);
            if (item == null) return false;
            item.Activo = false;
            return true;
        }
    }
}
