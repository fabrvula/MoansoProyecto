using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entProforma
    {
        public int IdProforma { get; set; }
        public string NroProforma { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }  // Pendiente, Aprobada, Anulada
        public decimal Total { get; set; }
        public string Observaciones { get; set; }
        public List<entDetalleProforma> Detalles { get; set; }

        public entProforma()
        {
            Detalles = new List<entDetalleProforma>();
        }
    }
}
